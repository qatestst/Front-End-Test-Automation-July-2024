using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsRegistryTests.Pages
{
    public class ViewStudentsPage : BasePage
    {
        public ViewStudentsPage(IWebDriver driver) : base(driver)
        {            
        }

        public override string PageUrl => "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:82/students";

        public IReadOnlyCollection<IWebElement> ListItemsStudents => driver.FindElements(By.CssSelector("Body > ul > li"));

        public string[] GetRegisterStudents()
        {
            var elementsStudents = this.ListItemsStudents.Select(x => x.Text).ToArray();
            return elementsStudents;
        }
    }
}
