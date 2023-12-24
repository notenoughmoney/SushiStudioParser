using HtmlAgilityPack;

namespace SushiStudioParser
{
    public class Parser
    {
        private Dictionary<string, string> XPathExpressions = new Dictionary<string, string>()
        {
            {"name",        ".//h3[contains(@class,'c-title')]"},
            {"description", ".//div[@class='q-mt-xs text-grey-6 s-font-xs s-font-md-sm']"},
            {"type",        ".//h2[@class='leading-none q-ma-none text-weight-boldest s-font-3xl s-font-md-4xl s-font-lg-2xl s-font-xl-3xl']/a"},
            {"cost",        ".//div[contains(@class,'i-price__discounted')]"},
            {"weight",      ".//div[contains(@class,'i-measure')]"}
        };

        public List<Product> GetProducts() 
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://irkutsk.sushi-studio.ru/");
            var sectionsNodes = doc.DocumentNode.SelectNodes("//div[@class='scrollama__steps']/div");
            List<Product> products = new List<Product>();

            // пробегаемся по секциям
            foreach (var sectionNode in sectionsNodes)
            {
                HtmlNodeCollection? productsNodes = sectionNode.SelectNodes(".//div[contains(@class,'com-product')]");
                if (productsNodes is null) continue;

                foreach (HtmlNode productNode in productsNodes)
                {
                    Product product     = new Product();

                    product.Name        = productNode.SelectSingleNode(XPathExpressions["name"]).InnerText.Trim();

                    product.Description = productNode.SelectSingleNode(XPathExpressions["description"])?.InnerText.Trim();

                    product.Type        = sectionNode.SelectSingleNode(XPathExpressions["type"]).InnerText.Trim();

                    product.Cost        = int.Parse(productNode.SelectSingleNode(XPathExpressions["cost"]).InnerText.Replace("₽", "").Trim());

                    if (productNode.SelectSingleNode(XPathExpressions["weight"]) is null) product.Weight = null;
                    else product.Weight = int.Parse(productNode.SelectSingleNode(XPathExpressions["weight"]).InnerText.Replace("/", "").Replace("гр.", "").Trim());

                    if (product.Cost is not null && product.Weight is not null && product.Cost != 0) // Проверка на null и деление на 0
                    {
                        product.Ratio = (float)product.Weight / (float)product.Cost; // Приведение к типу с плавающей точкой перед делением
                    }
                    else
                    {
                        product.Ratio = null; // Если деление невозможно, присваиваем null
                    }

                    products.Add(product);
                }
            }

            return products;
        }
    }
}