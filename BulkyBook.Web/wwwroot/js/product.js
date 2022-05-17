var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#table').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            {
                "data": "title", className: 'dt-body-left'
            },
            {
                "data": "isbn", className: 'dt-body-left'
            },
            {
                "data": "price", className: 'dt-body-left'
            },
            {
                "data": "author", className: 'dt-body-left'
            },
            {
                "data": "category.name", className: 'dt-body-left'
            },
        ]
    });
}
