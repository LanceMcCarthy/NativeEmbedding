using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

#if __IOS__
using CoreGraphics;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using TelerikUI;
using Foundation;
#endif

#if __ANDROID__
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Com.Telerik.Widget.Chart.Visualization.CartesianChart;
using Com.Telerik.Widget.Chart.Visualization.CartesianChart.Series.Categorical;
using Com.Telerik.Widget.Chart.Visualization.CartesianChart.Axes;
using Demo.Models;
#endif

#if WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Xamarin.Forms.Platform.UWP;
using Telerik.UI.Xaml.Controls.Chart;
using Demo.Models;
#endif

namespace Demo
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

#if __IOS__
            
            // UI FOR XAMARIN iOS native controls

            RootGrid.Padding = new Thickness(0, 20, 0, 0);

            var explanation1 = new UILabel
            {
                MinimumFontSize = 14f,
                Lines = 0,
                LineBreakMode = UILineBreakMode.WordWrap,
                Text = "This is the native iOS TKChart",
            };
            HeaderGrid.Children.Add(explanation1);

            // Chart
            TKChart chart = new TKChart();
            chart.AllowAnimations = true;
            chart.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;

            // Sample Data
            Random r = new Random();
            var list = Enumerable.Range(1, 12).Select(i => new TKChartDataPoint(new NSNumber(i), new NSNumber(r.Next() % 2000))).ToArray();

            // Add Series
            TKChartLineSeries series = new TKChartLineSeries(list);
            series.Selection = TKChartSeriesSelection.Series;
            chart.AddSeries(series);

            BodyGrid.Children.Add(chart);
#endif

#if __ANDROID__

            // UI FOR XAMARIN Android native controls

            HeaderGrid.Children.Add(new TextView(Forms.Context)
            {
                Text = "This is the native Android RadCartesianChartView",
                TextSize = 14
            });

            RadCartesianChartView chartView = new RadCartesianChartView(Forms.Context);
            chartView.HorizontalAxis = new CategoricalAxis();
            chartView.VerticalAxis = new LinearAxis();

		    // Sample Data
		    var monthResults = new Java.Util.ArrayList();
		    monthResults.Add(new MonthResult("Jan", 12));
		    monthResults.Add(new MonthResult("Feb", 5));
		    monthResults.Add(new MonthResult("Mar", 10));
		    monthResults.Add(new MonthResult("Apr", 7));
            
		    chartView.Series.Add(new LineSeries
		    {
		        CategoryBinding = new MonthResultDataBinding("Month"),
		        ValueBinding = new MonthResultDataBinding("Result"),
		        Data = monthResults
		    });
            
            BodyGrid.Children.Add(chartView);

#endif

#if WINDOWS_UWP


            // UI FOR XAMARIN UWP native controls
		    var explanation1 = new TextBlock
		    {
		        Text = "The next control is a CustomControl (a customized TextBlock with a bad ArrangeOverride implementation).",
		        FontSize = 14,
		        FontFamily = new FontFamily("HelveticaNeue"),
		        TextWrapping = TextWrapping.Wrap
		    };

		    HeaderGrid.Children.Add(explanation1);


            // Set up chart
		    var chartView = new RadCartesianChart();
		    chartView.HorizontalAxis = new CategoricalAxis();
		    chartView.VerticalAxis = new LinearAxis();

            // sample data
		    var monthResults = new List<MonthResult> {new MonthResult("Jan", 12), new MonthResult("Feb", 5), new MonthResult("Mar", 10), new MonthResult("Apr", 7)};

		    chartView.Series.Add(new LineSeries
		    {
		        CategoryBinding = new PropertyNameDataPointBinding("Month"),
		        ValueBinding = new PropertyNameDataPointBinding("Result"),
		        ItemsSource = monthResults
            });

            // Add native chart to Visual Tree
            BodyGrid.Children.Add(chartView);

#endif
        }

#if __IOS__
        SizeRequest? FixSize(NativeViewWrapperRenderer renderer, double width, double height)
        {
            var uiView = renderer.Control;

            if (uiView == null)
            {
                return null;
            }

            var constraint = new CGSize(width, height);

            // Let the CustomControl determine its size (which will be wrong)
            var badRect = uiView.SizeThatFits(constraint);

            // Use the width and substitute the height
            return new SizeRequest(new Size(badRect.Width, 70));
        }
#endif

#if __ANDROID__
        SizeRequest? FixSize(NativeViewWrapperRenderer renderer, int widthConstraint, int heightConstraint)
	    {
	        var nativeView = renderer.Control;

	        if ((widthConstraint == 0 && heightConstraint == 0) || nativeView == null)
	        {
	            return null;
	        }

	        int width = Android.Views.View.MeasureSpec.GetSize(widthConstraint);
	        int widthSpec = Android.Views.View.MeasureSpec.MakeMeasureSpec(width * 2, Android.Views.View.MeasureSpec.GetMode(widthConstraint));
	        nativeView.Measure(widthSpec, heightConstraint);
            
	        return new SizeRequest(new Size(nativeView.MeasuredWidth, nativeView.MeasuredHeight));
	    }
#endif
    }
}
