$(document).ready(function () {
    $('.selectTwo').select2({
        placeholder: 'Select an option'
    });
    //$('#sweet-success').on('click', function () {
    //    Swal.fire(
    //        'Good job!',
    //        'You clicked the button!',
    //        'success'
    //    )
    //})

    //$("#kt_sweetalert_demo_3_3").click(function (e) {
    //    Swal.fire("Good job!", "You clicked the button!", "success");
    //});

    $('#sweet-delete').on('click', function () {
        //const swalWithBootstrapButtons = Swal.mixin({
        //    customClass: {
        //        confirmButton: 'btn btn-success',
        //        cancelButton: 'btn btn-danger'
        //    },
        //    buttonsStyling: false
        //})

        //swalWithBootstrapButtons.fire({
        //    title: 'Are you sure?',
        //    text: "You won't be able to revert this!",
        //    icon: 'warning',
        //    showCancelButton: true,
        //    confirmButtonText: 'Yes, delete it!',
        //    cancelButtonText: 'No, cancel!',
        //    reverseButtons: true
        //}).then((result) => {
        //    if (result.isConfirmed) {
        //        swalWithBootstrapButtons.fire(
        //            'Deleted!',
        //            'Your file has been deleted.',
        //            'success'
        //        )
        //    } else if (
        //        /* Read more about handling dismissals below */
        //        result.dismiss === Swal.DismissReason.cancel
        //    ) {
        //        swalWithBootstrapButtons.fire(
        //            'Cancelled',
        //            'Your imaginary file is safe :)',
        //            'error'
        //        )
        //    }
        //})
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                Swal.fire(
                    'Deleted!',
                    'Your file has been deleted.',
                    'success'
                )
            }
        })
    })

    //$("#kt_sweetalert_demo_8").click(function (e) {
    //    Swal.fire({
    //        title: "Are you sure?",
    //        text: "You won't be able to revert this!",
    //    icon: "warning",
    //        showCancelButton: true,
    //        confirmButtonText: "Yes, delete it!"
    //    }).then(function (result) {
    //        if (result.value) {
    //            Swal.fire(
    //                "Deleted!",
    //                "Your file has been deleted.",
    //                "success"
    //            )
    //        }
    //    });
    //});


    
});
