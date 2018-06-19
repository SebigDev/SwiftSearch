function initialize() {
    var mapCanvas = document.getElementById('property-address-map'),
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
    var marker = new google.maps.Marker({
        position: latlang,
        map: map,
        icon: 'images/icons/map-pin-2.png'
    });
}
google.maps.event.addDomListener(window, 'load', initialize);