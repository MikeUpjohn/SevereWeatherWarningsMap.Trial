// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    mapboxgl.accessToken = 'pk.eyJ1IjoibWlrZXVwam9obiIsImEiOiJjazk2enRjbHQwODB5M2xtanB6bGtoOW9zIn0.QKZt26yxRxYmzMa6i1RkYQ';

    const map = new mapboxgl.Map({
        container: 'map',
        style: 'mapbox://styles/mapbox/streets-v12',
        center: [-96.94712405895336, 32.7680523410678],
        zoom: 6
    });

    $.get('/SevereWarnings/FloodWarnings', function (data) {
        $(data.features).each(function (i, dataItem) {
            if (dataItem.geometry != null) {
                console.log(i);
                map.addSource(dataItem.id, {
                    'type': 'geojson',
                    'data': {
                        'type': 'Feature',
                        'geometry': {
                            'type': 'Polygon',
                            // These coordinates outline Maine.
                            'coordinates': dataItem.geometry.coordinates
                        }
                    }
                });

                var isTornadoWarning = dataItem.properties.event == 'Tornado Warning';
                var fillColour = isTornadoWarning ? '#FF0000' : '#00ffff';
                var outlineColour = isTornadoWarning ? '#990033' : '#008080';

                // Add a new layer to visualize the polygon.
                map.addLayer({
                    'id': dataItem.id,
                    'type': 'fill',
                    'source': dataItem.id, // reference the data source
                    'layout': {},
                    'paint': {
                        'fill-color': fillColour, // blue color fill
                        'fill-opacity': 0.2
                    }
                });
                // Add a black outline around the polygon.
                map.addLayer({
                    'id': 'outline' + dataItem.id,
                    'type': 'line',
                    'source': dataItem.id,
                    'layout': {},
                    'paint': {
                        'line-color': outlineColour,
                        'line-width': 1
                    }
                });
            }
        });
    });
});