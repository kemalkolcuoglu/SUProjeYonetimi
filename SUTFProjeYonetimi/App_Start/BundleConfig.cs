using System.Web.Optimization;

namespace SUTFProjeYonetimi.App_Start
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/Content/css").Include(
				"~/Assets/css/bootstrap.css",
				"~/Assets/css/jquery-ui.min.css",
				"~/Assets/css/font-awesome.min.css",
				"~/Assets/css/smartadmin-production-plugins.min.css",
				"~/Assets/css/smartadmin-production.min.css",
				"~/Assets/css/smartadmin-production.min.css",
				"~/Assets/css/smartadmin-skins.min.css"));

			bundles.Add(new ScriptBundle("~/Scripts/js").Include(
				"~/Assets/js/libs/jquery-3.2.1.min.js",
				"~/Assets/js/libs/jquery-ui.min.js",
				//"~/Assets/js/jquery-ui.min.js",
				"~/Assets/js/picker.js",
				"~/Assets/js/picker.date.js",
				"~/Assets/js/app.config.js",
				"~/Assets/js/plugin/jquery-touch/jquery.ui.touch-punch.min.js",
				"~/Assets/js/bootstrap/bootstrap.min.js",
				"~/Assets/js/bootstrapvalidator/bootstrapValidator.min.js",
				"~/Assets/js/bootstrapvalidator/language/tr_TR.min.js",
				"~/Assets/js/notification/SmartNotification.min.js",
				"~/Assets/js/smartwidgets/jarvis.widget.min.js",
				"~/Assets/js/plugin/easy-pie-chart/jquery.easy-pie-chart.min.js",
				"~/Assets/js/plugin/sparkline/jquery.sparkline.min.js",
				"~/Assets/js/plugin/jquery-validate/jquery.validate.min.js",
				"~/Assets/js/plugin/bootstrap-slider/bootstrap-slider.min.js",
				"~/Assets/js/plugin/msie-fix/jquery.mb.browser.min.js",
				"~/Assets/js/plugin/fastclick/fastclick.min.js",
				"~/Assets/js/app.min.js",
				"~/Assets/js/smart-chat-ui/smart.chat.ui.min.js",
				"~/Assets/js/smart-chat-ui/smart.chat.manager.min.js",
				"~/Assets/js/plugin/flot/jquery.flot.cust.min.js",
				"~/Assets/js/plugin/flot/jquery.flot.resize.min.js",
				"~/Assets/js/plugin/flot/jquery.flot.time.min.js",
				"~/Assets/js/plugin/flot/jquery.flot.tooltip.min.js",
				"~/Assets/js/plugin/moment/moment.min.js",
				"~/Assets/js/plugin/fullcalendar/fullcalendar.min.js",
				"~/Assets/js/plugin/jquery-form/jquery-form.min.js",
				"~/Assets/js/plugin/jqgrid/jquery.jqGrid.min.js",
				"~/Assets/js/plugin/jqgrid/grid.locale-en.min.js"));

			BundleTable.EnableOptimizations = true;
		}
	}
}