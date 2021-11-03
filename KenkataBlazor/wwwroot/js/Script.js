

//product-quantity
//function hquantity() {
//    $(document).on('click', '.inc', function () {
//        $(this).prev().val(+$(this).prev().val() + 1);
//    });
//    $(document).on('click', '.dec', function () {
//        if ($(this).next().val() > 0) $(this).next().val(+$(this).next().val() - 1);
//    });
//}
//product-quantity


function initialize() {
    var latlng = new google.maps.LatLng(-10.45116724627934, 105.68893119880585);
    var options = {
        zoom: 16, center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("map"), options);

    var marker = new google.maps.Marker({
        position: latlng, title: "V�rt kontor!"
    });

    marker.setMap(map);
}

function sliderprice() {
    var slider = document.getElementById("myRange");
    var output = document.getElementById("demo");
    output.innerHTML = slider.value;
    slider.oninput = function () {
        output.innerHTML = this.value;
    }
}

function menuItemColor() {
    $(".prod-category").on("click", function () {
        $('.prod-category').children().children().removeClass('active');
        $(this).children().children().addClass("active");
        $(this).siblings().removeClass("active");
        $(this).addClass("active");
    });
};

var windowSmallBreakpoint = window.matchMedia("(max-width: 700px)");
windowSmallBreakpoint.addListener(filterCollapse);

function filterCollapse() {
    if (windowSmallBreakpoint.matches) {
        $("#filterCollapseDiv").removeClass("show");
        $("#filterCollapseDiv").addClass("mt-3");
    }
    else {
        $("#filterCollapseDiv").removeClass("mt-3");
        $("#filterCollapseDiv").addClass("show");
    }
}

function navSidebarCollapse() {
    $('#navSidebarCollapse').on('click', function () {
        $('#overlay').toggleClass('darken');
        $('#nav-sidebar').removeClass('collapsing');
        if ($('#nav-sidebar').hasClass('active')) {
            $('#nav-sidebar').toggleClass('collapsing');
        }
        $('#nav-sidebar').toggleClass('active');
    });
}

