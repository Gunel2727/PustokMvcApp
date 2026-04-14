


$(document).ready(function () {
    $(document).on('click', '.bookModalIcon', function (e) {
        e.preventDefault();
        let url = $(this).attr('href');
        if (!url) return;

        fetch(url)
            .then(res => res.text())
            .then(data => {
                $('#quickModal .modal-dialog').html(data);

               
                try {
                    const firstSlider = {
                        slidesToShow: 1,
                        arrows: false,
                        fade: true,
                        draggable: false,
                        swipe: false,
                        asNavFor: '.product-slider-nav'
                    };

                    const secondSlider = {
                        infinite: true,
                        autoplay: true,
                        autoplaySpeed: 8000,
                        slidesToShow: 4,
                        arrows: true,
                        prevArrow: { buttonClass: 'slick-prev', iconClass: 'fa fa-chevron-left' },
                        nextArrow: { buttonClass: 'slick-next', iconClass: 'fa fa-chevron-right' },
                        asNavFor: '.product-details-slider',
                        focusOnSelect: true
                    };

                    if ($.fn.slick) {
                        $('.product-details-slider').slick(firstSlider);
                        $('.product-slider-nav').slick(secondSlider);
                    }
                } catch (err) {
                    console.warn('Slider init error', err);
                }


                if ($.fn.modal) {
                    $('#quickModal').modal('show');
                } else {
                    console.warn('Bootstrap modal plugin not found.');
                }
            })
            .catch(err => console.error('Error loading modal content:', err));
    });

});
