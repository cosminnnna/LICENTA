/*// recycling.js

// Funcția pentru a face cererea AJAX și a adăuga marcatorii pe hartă
function displayRecyclingCenters(category) {
    // Faceți cererea AJAX către controller pentru a obține centrele de reciclare în funcție de categoria selectată
    $.ajax({
        url: '/RecyclingCenter/GetRecyclingCenters',
        method: 'GET',
        data: { category: category },
        success: function (data) {
            // Ștergeți marcatorii vechi de pe hartă (dacă există)
            deleteMarkers();

            // Iterați prin fiecare centru de reciclare și adăugați marcatorul corespunzător pe hartă
            data.forEach(function (center) {
                var position = { lat: center.latitude, lng: center.longitude };
                addMarker(position, center.name);
            });
        },
        error: function (xhr, status, error) {
            console.error('Eroare în obținerea datelor despre centrele de reciclare:', error);
        }
    });
}

// Funcția pentru a adăuga un marcator pe hartă
function addMarker(position, title) {
    var marker = new google.maps.Marker({
        position: position,
        map: map,
        title: title
    });
    markers.push(marker); // Adăugați marcatorul la lista de marcatori
}

// Funcția pentru a șterge toți marcatorii de pe hartă
function deleteMarkers() {
    markers.forEach(function (marker) {
        marker.setMap(null); // Eliminați marcatorul de pe hartă
    });
    markers = []; // Goliți lista de marcatori
}

// Adăugați un eveniment de click pentru fiecare buton de categorie
document.getElementById('paperButton').addEventListener('click', function () {
    displayRecyclingCenters('hartie');
});

document.getElementById('plasticButton').addEventListener('click', function () {
    displayRecyclingCenters('plastic');
});

document.getElementById('glassButton').addEventListener('click', function () {
    displayRecyclingCenters('sticla');
});

document.getElementById('metalButton').addEventListener('click', function () {
    displayRecyclingCenters('metal');
});
*/

// Locațiile pentru fiecare categorie
var locationsByCategory = {
    "hartie": [
        { lat: 40.7128, lng: -74.0060 }, // Exemplu locație pentru hârtie
        // Alte locații pentru hârtie...
    ],
    "plastic": [
        { lat: 34.0522, lng: -118.2437 }, // Exemplu locație pentru plastic
        // Alte locații pentru plastic...
    ],
    // Definiți și alte categorii și locații...
};

// Funcția care adaugă markere pe hartă pentru o anumită categorie
function addMarkersForCategory(map, category) {
    // Ștergeți markerele existente de pe hartă (opțional)
    // ...

    // Verificați dacă există locații pentru categoria selectată
    if (locationsByCategory.hasOwnProperty(category)) {
        // Parcurgeți fiecare locație și adăugați un marker pentru fiecare pe hartă
        locationsByCategory[category].forEach(function (location) {
            var marker = new google.maps.Marker({
                position: location,
                map: map,
                title: category + ' Location' // Titlu personalizat pentru marker
            });
        });
    }
}

// Inițializați harta și adăugați markere pentru categoria implicită la încărcarea paginii
function initMap() {
    var map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: 40.7128, lng: -74.0060 }, // Centrul hartii (exemplu: New York City)
        zoom: 8
    });

    // Adăugați markere pentru categoria implicită (exemplu: "hartie")
    addMarkersForCategory(map, 'hartie');

    // Asociați funcții de click cu fiecare buton pentru a afișa markerele pentru categoria corespunzătoare
    document.getElementById('hartieButton').addEventListener('click', function () {
        addMarkersForCategory(map, 'hartie');
    });

    document.getElementById('plasticButton').addEventListener('click', function () {
        addMarkersForCategory(map, 'plastic');
    });

    // Adăugați și pentru celelalte butoane și categorii...
}
