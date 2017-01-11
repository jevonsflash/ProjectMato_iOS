using System;
using UIKit;

namespace ProjectMato.iOS
{
	public class Application
	{
		static void Main (string[] args)
		{
		    try
		    {
			    UIApplication.Main(args,null,"AppDelegate");

		    }
		    catch (Exception e)
		    {
		        
		        throw;
		    }
		}
	}
}

