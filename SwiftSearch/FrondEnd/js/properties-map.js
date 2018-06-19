function initialize(icon) {

    var propertyIcon = 'images/icons/map-pin-2.png';
    if (typeof icon != 'undefined' && 'default' != icon  ){
        propertyIcon = 'images/icons/map-pin-2-' + icon + '.png';
    }

    var properties = [
        [51.4912,-0.2324863],
        [51.4720689,-0.1149035],
        [51.436377,0.1281922],
        [51.5067246,-0.1304893],
        [51.4919392,0.1296363],
        [51.407961,-0.02594],
        [51.4844009,0.050243],
        [51.4273723,-0.2413595]
    ];
    var mapOptions = {
        center: new google.maps.LatLng(40.7033127, -73.979681),
        zoom: 12,
        maxZoom: 15,
        zoomControl: true,
        zoomControlOptions: {
            style: google.maps.ZoomControlStyle.SMALL
        },
        panControl: false,
        mapTypeControl: true,
        scrollwheel: false,
        scaleControl: false,
        streetViewControl: false,
        fullscreenControl: true,
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
    var map = new google.maps.Map(document.getElementById('property-google-map'), mapOptions);
    var bounds = new google.maps.LatLngBounds();
    var myOptions = {
        disableAutoPan: false
        ,maxWidth: 0
        ,pixelOffset: new google.maps.Size( -145, -450)
        ,zIndex: null
        ,boxStyle: {
            width: "280px"
        }
        ,closeBoxMargin: "0 0 -16px -16px"
        ,closeBoxURL: "images/icons/close.png"
        ,infoBoxClearance: new google.maps.Size(1, 1)
        ,isHidden: false
        ,pane: "floatPane"
        ,enableEventPropagation: false
    };

    var  marker, i;
    for( i = 0; i < properties.length; i++ ) {
        var position = new google.maps.LatLng(properties[i][0], properties[i][1]);

        bounds.extend(position);

        marker = new google.maps.Marker({
            map: map,
            draggable: true,
            position: position,
            icon: propertyIcon,
            visible: true
        });

        google.maps.event.addListener(marker, 'click', (function(marker, i) {
            return function() {
                infoWindowContent  = '<div class="property-infoBox property-post property-post property-post-grid clearfix">'
                    +'<div class="property-thumbnail">'
                    +'<a href="single-property.html"><img src="images/demo/property/1.jpg" alt="Property"></a>'
                    +'</div>'
                    +'<div class="property-wrap">'
                    +'<header class="property-header clearfix">'
                    +'<h2 class="property-title"><a href="single-property.html">Beach House</a></h2>'
                    +'<p class="property-location">1348 209th St, Flushing</p>'
                    +'</header>'
                    +'<ul class="property-meta list-unstyled"><li>Bads: 3</li><li>Baths: 4</li><li>Sqft: 1,500</li></ul>'
                    +'<footer class="property-footer"><span class="property-price"><span>$</span>787,000</span></footer>'
                    +'</div>'
                    +'</div>';

                myOptions.content = infoWindowContent;
                var ib = new InfoBox(myOptions);
                ib.open(map, marker);
            }
        })(marker, i));
    }

    map.fitBounds(bounds);
    map.panToBounds(bounds);

    google.maps.event.addDomListener(window, "resize", function() {
        map.fitBounds(bounds);
        map.panToBounds(bounds);
    });

    // Hide Load when map is fully loaded.
    google.maps.event.addListenerOnce( map, 'idle', function(){
        var loader = document.getElementById('property-google-map-loader');
        loader.style.display = 'none';
    });
}

google.maps.event.addDomListener(window, 'load', function(){
    initialize(window.pearlPropertyIcon);
});

// Only for demo purpose
document.addEventListener("DOMContentLoaded", function(event) {
    var switcher = document.getElementById("switcher");
    var items = switcher.childNodes;
    for (var i = 0; i < items.length; i++) {
        items[i].addEventListener("click", function(){
            initialize(this.dataset.theme);
        });
    }
});