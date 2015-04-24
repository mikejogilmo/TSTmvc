// Morris.js Charts sample data for SB Admin template

$(function() {

    // Area Chart
    Morris.Area({
        element: 'morris-area-chart',
        data: [{
            period: '2013 Q1',
            Cardstock: 2666,
            Paper: null,
            Labels: 2647
        }, {
            period: '2013 Q2',
            Cardstock: 2778,
            Paper: 2294,
            Labels: 2441
        }, {
            period: '2013 Q3',
            Cardstock: 4912,
            Paper: 1969,
            Labels: 2501
        }, {
            period: '2013 Q4',
            Cardstock: 3767,
            Paper: 3597,
            Labels: 5689
        }, {
            period: '2014 Q1',
            Cardstock: 6810,
            Paper: 1914,
            Labels: 2293
        }, {
            period: '2014 Q2',
            Cardstock: 5670,
            Paper: 4293,
            Labels: 1881
        }, {
            period: '2014 Q3',
            Cardstock: 4820,
            Paper: 3795,
            Labels: 1588
        }, {
            period: '2014 Q4',
            Cardstock: 15073,
            Paper: 5967,
            Labels: 5175
        }, {
            period: '2015 Q1',
            Cardstock: 10687,
            Paper: 4460,
            Labels: 2028
        }, {
            period: '2015 Q2',
            Cardstock: 8432,
            Paper: 5713,
            Labels: 1791
        }],
        xkey: 'period',
        ykeys: ['Cardstock', 'Paper', 'Labels'],
        labels: ['Cardstock', 'Paper', 'iPod Touch'],
        pointSize: 2,
        hideHover: 'auto',
        resize: true
    });

    // Donut Chart
    Morris.Donut({
        element: 'morris-donut-chart',
        data: [{
            label: "Download Sales",
            value: 12
        }, {
            label: "In-Store Sales",
            value: 30
        }, {
            label: "Mail-Order Sales",
            value: 20
        }],
        resize: true
    });

    // Line Chart
    Morris.Line({
        // ID of the element in which to draw the chart.
        element: 'morris-line-chart',
        // Chart data records -- each entry in this array corresponds to a point on
        // the chart.
        data: [{
            d: '2015-10-01',
            visits: 802
        }, {
            d: '2015-10-02',
            visits: 783
        }, {
            d: '2015-10-03',
            visits: 820
        }, {
            d: '2015-10-04',
            visits: 839
        }, {
            d: '2015-10-05',
            visits: 792
        }, {
            d: '2015-10-06',
            visits: 859
        }, {
            d: '2015-10-07',
            visits: 790
        }, {
            d: '2015-10-08',
            visits: 1680
        }, {
            d: '2015-10-09',
            visits: 1592
        }, {
            d: '2015-10-10',
            visits: 1420
        }, {
            d: '2015-10-11',
            visits: 882
        }, {
            d: '2015-10-12',
            visits: 889
        }, {
            d: '2015-10-13',
            visits: 819
        }, {
            d: '2015-10-14',
            visits: 849
        }, {
            d: '2015-10-15',
            visits: 870
        }, {
            d: '2015-10-16',
            visits: 1063
        }, {
            d: '2015-10-17',
            visits: 1192
        }, {
            d: '2015-10-18',
            visits: 1224
        }, {
            d: '2015-10-19',
            visits: 1329
        }, {
            d: '2015-10-20',
            visits: 1329
        }, {
            d: '2015-10-21',
            visits: 1239
        }, {
            d: '2015-10-22',
            visits: 1190
        }, {
            d: '2015-10-23',
            visits: 1312
        }, {
            d: '2015-10-24',
            visits: 1293
        }, {
            d: '2015-10-25',
            visits: 1283
        }, {
            d: '2015-10-26',
            visits: 1248
        }, {
            d: '2015-10-27',
            visits: 1323
        }, {
            d: '2015-10-28',
            visits: 1390
        }, {
            d: '2015-10-29',
            visits: 1420
        }, {
            d: '2015-10-30',
            visits: 1529
        }, {
            d: '2015-10-31',
            visits: 1892
        }, ],
        // The name of the data record attribute that contains x-visitss.
        xkey: 'd',
        // A list of names of data record attributes that contain y-visitss.
        ykeys: ['visits'],
        // Labels for the ykeys -- will be displayed when you hover over the
        // chart.
        labels: ['Visits'],
        // Disables line smoothing
        smooth: false,
        resize: true
    });

    // Bar Chart
    Morris.Bar({
        element: 'morris-bar-chart',
        data: [{
            device: 'Cardstock',
            geekbench: 136
        }, {
            device: 'Cardstock 3G',
            geekbench: 137
        }, {
            device: 'Cardstock 3GS',
            geekbench: 275
        }, {
            device: 'Cardstock 4',
            geekbench: 380
        }, {
            device: 'Cardstock 4S',
            geekbench: 655
        }, {
            device: 'Cardstock 5',
            geekbench: 1571
        }],
        xkey: 'device',
        ykeys: ['geekbench'],
        labels: ['Geekbench'],
        barRatio: 0.4,
        xLabelAngle: 35,
        hideHover: 'auto',
        resize: true
    });


});
