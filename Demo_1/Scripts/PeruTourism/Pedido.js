$(function () {

     

    var grid = $('#resultados').DataTable({
        scrollX: true,
        paging: true,
        //responsive: true,
        processing: true,
        ordering: false,
        deferLoading: 0,
        responsive: {
            details: {
                type: 'column',
                display: $.fn.dataTable.Responsive.display.childRowImmediate,
                renderer: function (api, index, columns) {
                    $('div#resultados_wrapper .dataTables_scrollHead').hide();

                    var row = $(api.row(index).node());
                    row.hide();

                    var html = $('#responsive-template').html();
                    //var a = document.getElementById('yourlinkId'); //or grab it by tagname etc

                 
                    var template = $(html);

                    template.find('#pedido').html(columns[1].data);
                    template.find('#alias').html(columns[2].data);

                    template.find('#ruc').html(columns[9].data);
                    template.find('#paginaweb').html(columns[8].data);


                    template.find('#pais').html(columns[5].data);
                    template.find('#ciudad').html(columns[6].data);
                    template.find('#idioma').html(columns[10].data);
                    //setTextColor(template, '#estado', columns[11].data);


                    return template;
                }
            }
        },

        ajax: {
            method: 'GET',
            url: '/Home/ListadoPedido',
            dataType: 'json',
            dataSrc: ''
            //data: function (items) {

            //    //var filtro = {
            //    //    Estado: $.trim($('#proveedor_estado').val())
            //    //};
            //    debugger
            //    var filtro = items;
            //    return filtro;
            //}
        },

        columns: [
            //{
            //    title: 'PROVEEDOR',
            //    data: 'PROVEEDOR',
            //    width: 70,
            //    className: 'not-mobile',
            //    visible: false,
            //},
            {
                title: 'PEDIDO',
                data: 'NroPedido',
                width: 50,
                className: 'not-mobile'
            },

            {
                title: 'DESCRIPCION PEDIDO',
                data: 'DesPedido',
                width: 50,
                className: 'not-mobile'
            },

            //{
            //    title: 'TIPO PROVEEDOR',
            //    data: 'TPROVEEDOR',
            //    width: 70,
            //    className: 'not-mobile',
            //    visible: false,
            //},

            //{
            //    title: 'TIPO',
            //    data: 'TIPO',
            //    width: 125,
            //    className: 'not-mobile',
            //    visible: false,
            //},
            {
                title: 'FECHA PEDIDO',
                data: 'FchPedido',
                width: 50,
                className: 'not-mobile'
            }

            //{
            //    title: 'CIUDAD',
            //    data: 'CIUDAD',
            //    width: 70,
            //    className: 'not-mobile'
            //},

            //{
            //    title: 'DIRECCION',
            //    data: 'DIRECCION',
            //    width: 150,
            //    className: 'not-mobile',
            //    visible: false,
            //},

            //{
            //    title: 'PAGINA WEB',
            //    data: 'PAGINAWEB',
            //    width: 125,
            //    className: 'not-mobile',
            //    visible: false,
            //},
            //{
            //    title: 'RUC',
            //    data: 'RUC',
            //    width: 125,
            //    className: 'not-mobile',
            //    visible: false,
            //},

            //{
            //    title: 'IDIOMA',
            //    data: 'IDIOMA',
            //    width: 70,
            //    className: 'not-mobile',
            //    visible: true,
            //},

            //{
            //    title: 'ESTADO',
            //    data: 'ESTADO',
            //    width: 20,
            //    className: 'not-mobile',
            //    visible: true,
            //    render: renderTextColor
            //},


            //{
            //    data: null,
            //    width: 80,
            //    className: 'dt-body-center not-mobile',
            //    render: function (data, type, row, meta) {
            //        var content = [];


            //        var SeleccionarOpcion = '<a class="btn btn-warning btn-SeleccionarOpcion data-toggle="modal" data-target="#myModal" title="tools"><i class="fa fa-cogs"></i></a>';
            //        var CrearServicio = '<button class="btn btn-primary btn-VerServicio" title="view service"><i class="fa fa-eye-slash"></i></button>';
            //        //var CrearTarifa = '<button class="btn btn-danger btn-VerTarifa" title="Ver Tarifa"><i class="fa fa-file-text-o"></i></button>';


            //        content.push(CrearServicio);
            //        content.push(SeleccionarOpcion);

            //        return content.join('&nbsp;&nbsp;');
            //    }
            //},

        ],

    });

















});