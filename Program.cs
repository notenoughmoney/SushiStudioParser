using HtmlAgilityPack;
using SushiStudioParser;

class Parser
{
    static void Main (string[] args)
    {
        HtmlWeb web = new HtmlWeb ();
        HtmlDocument doc = web.Load("https://irkutsk.sushi-studio.ru/");
        var sectionsNodes = doc.DocumentNode.SelectNodes("//div[@class='scrollama__steps']/div");
        List<Product> products = new List<Product>();
        
        // пробегаемся по секциям
        foreach (var sectionNode in sectionsNodes)
        {
            HtmlNodeCollection? productsNodes = sectionNode.SelectNodes(".//div[contains(@class,'com-product')]").Where(x => x is not null).;
            if (productsNodes is not null) Console.WriteLine(productsNodes.Count);

            foreach (HtmlNode productNode in productsNodes)
            {
                if (productNode is null) continue;
                Product product = new Product();
                product.Name        = productNode.SelectSingleNode(".//h3[contains(@class,'c-title')]").InnerText;
                product.Description = productNode.SelectSingleNode(".//div[@class='q-mt-xs text-grey-6 s-font-xs s-font-md-sm']").InnerText;
                product.Type        = sectionNode.SelectSingleNode(".//h2[@class='leading-none q-ma-none text-weight-boldest s-font-3xl s-font-md-4xl s-font-lg-2xl s-font-xl-3xl']/a").InnerText;
                product.Cost        = sectionNode.SelectSingleNode(".//div[contains(@class,'i-price__discounted')]").InnerText;
                product.Weignt      = sectionNode.SelectSingleNode(".//div[contains(@class,'i-measure')]").InnerText;
                Console.WriteLine(product.Name);
                Console.WriteLine(product.Description);
                Console.WriteLine(product.Type);
                Console.WriteLine(product.Cost);
                Console.WriteLine(product.Weignt);
            }

        }
        
    }
}