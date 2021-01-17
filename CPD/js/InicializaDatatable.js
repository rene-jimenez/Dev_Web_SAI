function InicializarDatable(idTabla)
{
    $(idTabla).DataTable({
        ordering: true,
        paging: true,
       
    },
    {
            language: {
            url: 'js/Spanish.json'
        }
    });
}