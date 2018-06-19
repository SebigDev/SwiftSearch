function initialize() {
    var mapCanvas = document.getElementById('property-address-map');
    var latlang = new google.maps.LatLng(40.7033127, -73.979681);
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
    var input = document.getElementById('searchInput');
    var searchBox = new google.maps.places.SearchBox(input);

    map.addListener('bounds_changed', function () {
        searchBox.setBounds(map.getBounds());
    });

    var markers = [];

    searchBox.addListener('places_changed', function () {
        var places = searchBox.getPlaces();

        if (places.length == 0) {
            return;
        }

        // Clear out the old markers.
        markers.forEach(function (marker) {
            marker.setMap(null);
        });
        markers = [];

        // For each place, get the icon, name and location.
        var bounds = new google.maps.LatLngBounds();
        places.forEach(function (place) {
            if (!place.geometry) {
                console.log("Returned place contains no geometry");
                return;
            }
            var icon = {
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25)
            };

            // Create a marker for each place.
            markers.push(new google.maps.Marker({
                map: map,
                icon: icon,
                title: place.name,
                position: place.geometry.location
            }));

            if (place.geometry.viewport) {
                // Only geocodes have viewport.
                bounds.union(place.geometry.viewport);
            } else {
                bounds.extend(place.geometry.location);
            }

            document.getElementById('address-map-location').value = place.geometry.location.lat() + ',' + place.geometry.location.lng();
        });

        map.fitBounds(bounds);
    });
}

$(function () {
    var $sortable = $("#sortable");
    $sortable.sortable({
        handle: ".fa-sort",
        cursor: "move"
    });
    $sortable.on('click', '.fa-times', function (event) {
        $(this).parent(".single-detail").remove();
    });

    $("#add-new-detail").on('click', function (event) {
        var newDetail =
            '<div class="single-detail ui-state-default">' +
            '<div class="row">' +
            '<div class="col-sm-6"><input type="text" name="titles[]" value=""></div>' +
            '<div class="col-sm-6"><input type="text" name="values[]" value=""></div>' +
            '</div><i class="fa fa-sort ui-sortable-handle"></i><i class="fa fa-times"></i>' +
            '</div>';
        $("#sortable").append(newDetail);
        event.preventDefault();
    });
});