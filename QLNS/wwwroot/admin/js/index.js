$(function() {
    "use strict";
		
    // chart 2
	var chartContainer = $('.chart-container-2');
	var revenue1 = chartContainer.data('revenue1');
	var revenue2 = chartContainer.data('revenue2');
	var revenue3 = chartContainer.data('revenue3');
	var revenue4 = chartContainer.data('revenue4');
		var ctx = document.getElementById("chart2").getContext('2d');
			var myChart = new Chart(ctx, {
				type: 'doughnut',
				data: {
					labels: ["rau sạch", "hạt", "củ quả", "mật"],
					datasets: [{
						backgroundColor: [
							"#ffffff",
							"rgba(255, 255, 255, 0.70)",
							"rgba(255, 255, 255, 0.50)",
							"rgba(255, 255, 255, 0.20)"
						],
						data: [revenue1, revenue2, revenue3, revenue4],
						borderWidth: [0, 0, 0, 0]
					}]
				},
			options: {
				maintainAspectRatio: false,
			   legend: {
				 position :"bottom",	
				 display: false,
				    labels: {
					  fontColor: '#ddd',  
					  boxWidth:15
				   }
				}
				,
				tooltips: {
				  displayColors:false
				}
			   }
			});
		

		
		
   });	 
   