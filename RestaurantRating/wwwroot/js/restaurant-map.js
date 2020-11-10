
const position = new google.maps.LatLng(lat, lon);
const mapOptions = {
    mapTypeId: 'roadmap',
    center: position,
    zoom: 17
};
const map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
map.setTilt(45);
const marker = new google.maps.Marker({ position, map, title });
const infoWindow = new google.maps.InfoWindow();
infoWindow.setContent(title);
infoWindow.open(map, marker);
