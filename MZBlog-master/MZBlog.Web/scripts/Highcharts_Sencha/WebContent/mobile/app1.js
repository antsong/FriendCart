Ext.Loader.setConfig({
    enabled : true,
    disableCaching : true, // For debug only
    paths : {
        'Chart' : '../Chart'
    }
});

Ext.require('Chart.ux.Highcharts');
//Ext.require('Chart.ux.ChartsMobileConfig');
//Ext.require('Highcharts.view.ResponseChart');

// ALWAYS POST!!
//Ext.override(Ext.data.proxy.Ajax,{ 
//    getMethod: function(request) { 
//        return 'POST'; 
//    } 
//});

//Ext.ns('Demo');

Ext.application({
    name: 'Highcharts',
    appFolder: 'app',
    requires:['Chart.ux.Highcharts',
             
              'Highcharts.view.SamplePanel',
             ],
    
    views:['Highcharts.view.SamplePanel'],
    models:[],
    stores:[],

    launch : function() {
    	debugger;

    	var panel = Ext.create('Highcharts.view.SamplePanel');

    	Ext.Viewport.add(panel);
    	

    }

});