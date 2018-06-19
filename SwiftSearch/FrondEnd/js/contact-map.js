function initialize() {
    var mapCanvas = document.getElementById('google-map'),
        latlang = new google.maps.LatLng(40.7033127, -73.979681);

    var options = {
        center: latlang,
        scrollwheel: false,
        zoom: 12,
        zoomControl: true,
        zoomControlOptions: {
            style: google.maps.ZoomControlStyle.SMALL
        },
        panControl: false,
        mapTypeControl: true,
        styles: [
            {
                "featureType": "all",
                "stylers": [
                    {
                        "saturation": 0
                    },
                    {
                        "hue": "#e7ecf0"
                    }
                ]
            },
            {
                "featureType": "road",
                "stylers": [
                    {
                        "saturation": -70
                    }
                ]
            },
            {
                "featureType": "transit",
                "stylers": [
                    {
                        "visibility": "off"
                    }
                ]
            },
            {
                "featureType": "poi",
                "stylers": [
                    {
                        "visibility": "off"
                    }
                ]
            },
            {
                "featureType": "water",
                "stylers": [
                    {
                        "visibility": "simplified"
                    },
                    {
                        "saturation": -60
                    }
                ]
            }
        ]
    };

    var map = new google.maps.Map(mapCanvas, options);
    var myOptions = {
        disableAutoPan: false
        , maxWidth: 0
        , pixelOffset: new google.maps.Size(-155, -250)
        , zIndex: null
        , boxStyle: {
            width: "300px"
        }
        , closeBoxMargin: "0 0 -16px -16px"
        , closeBoxURL: "images/icons/close.png"
        , infoBoxClearance: new google.maps.Size(1, 1)
        , isHidden: false
        , pane: "floatPane"
        , enableEventPropagation: false
    };

    var contentString = '<div class="contact-infoBox widget_contact">' +
        '<h4>Contact Details</h4>' +
        '<ul class="list-unstyled">' +
        '<li><i class="fa fa-map-marker"></i><span>227 W 27th St, New York, NY 10001</span></li>' +
        '<li><i class="fa fa-mobile"></i><span>+1 212-217-7999</span></li>' +
        '<li><i class="fa fa-envelope-o"></i><a href="mailto:support@homefind.com">support@homefind.com</a></li>' +
        '</ul>' +
        '</div>';

    var marker = new google.maps.Marker({
        position: latlang,
        map: map,
        icon: 'images/icons/map-pin.png',
        visible: true
    });

    marker.addListener('click', function () {
        myOptions.content = contentString;
        var ib = new InfoBox(myOptions);
        ib.open(map, marker);
    });
}
google.maps.event.addDomListener(window, 'load', initialize);