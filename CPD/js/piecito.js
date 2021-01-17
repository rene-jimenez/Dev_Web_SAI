
$(document).ready(function () {
    var pieData = [{
        data: 135,
        color: "#F44336",
        label: "Presidencia"
    }, {
        data: 15,
        color: "#245269",
        label: "Accion Electoral"
    }, {
        data: 30,
        color: "#8BC34A",
        label: "Organización"
    }, {
        data: 15,
        color: "#00bcd4",
        label: "Finanzas"
    }, {
        data: 15,
        color: "#009688",
        label: "Otras"
    }];
    $("#donut-chart")[0] && $.plot("#donut-chart", pieData, {
        series: {
            pie: {
                innerRadius: .5,
                show: !0,
                stroke: {
                    width: 2
                }
            }
        },
        legend: {
            container: ".flc-donut",
            backgroundOpacity: .5,
            noColumns: 0,
            backgroundColor: "white",
            lineWidth: 0
        },
        grid: {
            hoverable: !0,
            clickable: !0
        },
        tooltip: !0,
        tooltipOpts: {
            content: "%p.0%, %s",
            shifts: {
                x: 20,
                y: 0
            },
            defaultTheme: !1,
            cssClass: "flot-tooltip"
        }
    })
})