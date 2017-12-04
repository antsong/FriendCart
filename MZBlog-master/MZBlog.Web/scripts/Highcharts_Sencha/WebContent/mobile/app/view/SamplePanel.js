var store = Ext.create('Ext.data.JsonStore', {
					fields : ['time', 'yesterday', 'today'],
					data:[
				            {'time' : "jan", 'yesterday': 100.3, 'today': 112.3},
				            {'time' : "feb", 'yesterday': 150.3, 'today': 432.3},
				            {'time' : "mar", 'yesterday': 200.3, 'today': 231.3},
				            {'time' : "apr", 'yesterday': 132.3, 'today': 324.3},
				            {'time' : "may", 'yesterday': 309.3, 'today': 189.3},
				            {'time' : "jun", 'yesterday': 29.3,  'today': 123.3},
				            
				            ],
});
Ext.define('Highcharts.view.SamplePanel', {
	extend : 'Ext.Panel',
	 xtype:'samplepanel',
//	requires : [ 'AmapMobile.view.module.marketingdiagnostics.DiagnosticInnerHeadingPanel',
//	             'AmapMobile.view.module.marketingdiagnostics.Diagnostic'],

	config : {
		title: 'Sample',
		layout : {
			type : 'fit'			
		},
		style: 'background-color:white; border: 4px solid red',
//		width : 800,
//		height : 600,
		items : [
		     {
		    	 	xtype : 'highchart',
					id : 'chart',
					height : 500,
					width : 700,
					store : store,
				    series : [{
				      type : 'spline',
				      dataIndex : 'yesterday',
				      name : 'Yesterday'
				    }, {
				      type : 'spline',
				      dataIndex : 'today',
				      name : 'Today'
				    }],
				    xField : 'time',
				    chartConfig : {
				      chart : {
				        marginRight : 130,
				        marginBottom : 120,
				        zoomType : 'x'
				      },
				      title : {
				        text : 'Simple spline chart with animation',
				        x : -20 //center
				      },
				      subtitle : {
				        text : 'Random Value',
				        x : -20
				      },
				      xAxis : [{
				        title : {
				          text : 'Time',
				          margin : 20
				        },
				        labels : {
				          rotation : 270,
				          y : 35,
				          formatter : function () {
				            var dt = Ext.Date.parse (parseInt (this.value) / 1000, "U");
				            if (dt) {
				              return Ext.Date.format (dt, "H:i:s");
				            }
				            return this.value;
				          }

				        }
				      }],
				      yAxis : {
				        title : {
				          text : 'Temperature'
				        },
				        plotLines : [{
				          value : 0,
				          width : 1,
				          color : '#808080'
				        }]
				      },
				      tooltip : {
				        formatter : function () {
				          var dt = Ext.Date.parse (parseInt (this.x) / 1000, "U");
				          return 'At <b>' + this.series.name + '</b>' + Ext.Date.format (dt, "H:i:s") + ',<br/>temperature is : ' + this.y;
				        }

				      },
				      legend : {
				        layout : 'vertical',
				        align : 'right',
				        verticalAlign : 'top',
				        x : -10,
				        y : 100,
				        borderWidth : 0
				      },
				      credits : {
				        text : 'joekuan.wordpress.com',
				        href : 'http://joekuan.wordpress.com',
				        style : {
				          cursor : 'pointer',
				          color : '#707070',
				          fontSize : '12px'
				        }
				      }
				    }
				 
},		   
//		     { xtype:'diagnostic', id:'diagnostictabpanel'}
		     ]
		       
	}
});
