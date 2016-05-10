namespace IdentityWeb.Modules.Test
{
    using Nancy.Localization;
    using Nancy.ViewEngines;

    public class TestModule : Nancy.NancyModule
	{
		public TestModule(IViewEngine engine, ITextResource textResource) 
			: base("/test") 
		{
			this.Get ["/{name}/{age}"] = parameters => 
			{
				this.ViewBag.Title = "Here is my viewbag title!";
				var m = new TestModel();
				m.Age = parameters.age;
				m.Name = parameters.name;
				return this.View["Views/Test/RazorTest", m];
			};
		}
	}

	public class TestModel 
	{
		public string Name { get; set;}
		public int Age { get; set; }
	}
}

