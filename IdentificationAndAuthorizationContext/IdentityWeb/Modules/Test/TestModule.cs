using System;
using Nancy.ViewEngines;
using System.IO;
using Nancy.ViewEngines.Razor;
using Nancy.Localization;

namespace IdentityWeb
{
	public class TestModule : Nancy.NancyModule
	{
		private IViewEngine engine;

		public TestModule(IViewEngine engine, ITextResource textResource) 
			: base("/test") 
		{
			this.engine = engine;

			this.Get ["/{name}/{age}"] = parameters => 
			{
				ViewBag.Title = "Here is my viewbag title!";
				var m = new TestModel();
				m.Age = parameters.age;
				m.Name = parameters.name;
				return View["Views/Test/RazorTest", m];
			};
		}
	}

	public class TestModel 
	{
		public string Name { get; set;}
		public int Age { get; set; }
	}
}

