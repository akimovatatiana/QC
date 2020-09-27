using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.IO;
using HtmlAgilityPack;

namespace LinksChecker
{
    class Program
    {
        private const string HttpProtocol = "http";
        private const string Url = "http://91.210.252.240/broken-links/";

        private static int _validCounter = 0;
        private static int _invalidCounter = 0;

        private static bool IsLinkCorrect(string link)
        {
            return !string.IsNullOrEmpty(link) && !(link.StartsWith("#") || link.Contains(HttpProtocol)) ;
        }

        private static string FormFullLink(string link)
        {
            return Url + link; 
        }

        private static void PrintInfoToFiles(StreamWriter invalidLinksFile, StreamWriter validLinksFile)
        {
            invalidLinksFile.WriteLine($"\nInvalid links count: {_invalidCounter}\nCheck date: {DateTime.Now}");
            validLinksFile.WriteLine($"\nValid links count: {_validCounter}\nCheck date: {DateTime.Now}");
        }
        
        private static void CheckAndWriteLinkToFile(string link, StreamWriter invalidLinksFile, StreamWriter validLinksFile)
        {
            var fullLink = FormFullLink(link);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fullLink);
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string message = $"{fullLink} {response.StatusCode.GetHashCode()} {response.StatusDescription}";
                validLinksFile.WriteLine(message);
                _validCounter++;
            }
            catch (WebException e)
            {
                string message = $"{fullLink} {((HttpWebResponse)e.Response).StatusCode.GetHashCode()} {((HttpWebResponse)e.Response).StatusDescription}";
                invalidLinksFile.WriteLine(message);
                _invalidCounter++;
            }
        }

        public static HashSet<string> GetLinksFromSite(string link)
        {
            var web = new HtmlWeb();
            var document = web.Load(link);

            return document.DocumentNode.Descendants("a")
                .Select(a => a.GetAttributeValue("href", null))
                .Where(url => IsLinkCorrect(url))
                .ToHashSet();
        }

        public static List<string> GetAllLinks()
        {
            var links = new List<string>();
            var link = string.Empty;

            GetUniqueLinksFromSiteRecursively(link, links);

            return links;
        }

        private static void GetUniqueLinksFromSiteRecursively(string link, List<string> links)
        {
            var linksFromPage = GetLinksFromSite(FormFullLink(link));
            links.Add(link);
            
            if (linksFromPage.Any())
            {
                foreach (var l in linksFromPage.Where(l => !links.Contains(l)))
                {
                    GetUniqueLinksFromSiteRecursively(l, links);
                }
            }
        }

        static void Main()
        {
            using var invalidLinksFile = new StreamWriter("../../../invalid.txt");
            using var validLinksFile = new StreamWriter("../../../valid.txt");

            var links = GetAllLinks();

            foreach (var link in links)
            {
                CheckAndWriteLinkToFile(link, invalidLinksFile, validLinksFile);
            }

            PrintInfoToFiles(invalidLinksFile, validLinksFile);
        }
    }
}