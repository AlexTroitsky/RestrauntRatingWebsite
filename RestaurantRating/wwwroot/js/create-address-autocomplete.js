function initMap() {
    const input = document.getElementById("adress-autocomplete");
    const options = { types: ['address'], componentRestrictions: { country: 'il' } };
    const autocomplete = new google.maps.places.Autocomplete(input, options);
    // Set the data fields to return when the user selects a place.
    autocomplete.setFields(["address_components", "geometry", "name"]);
    autocomplete.addListener("place_changed", () => {
        const place = autocomplete.getPlace();

        if (!place.geometry) {
            window.alert("No details available for input: '" + place.name + "'");
            return;
        }

        if (place.address_components) {
            address = [
                (place.address_components[0] &&
                    place.address_components[0].short_name) ||
                "",
                (place.address_components[1] &&
                    place.address_components[1].short_name) ||
                "",
                (place.address_components[2] &&
                    place.address_components[2].short_name) ||
                "",
            ].join(" ");

        }

        document.getElementById("adress-city").value = place.address_components[1].short_name;
        document.getElementById("adress-coordinates-lat").value = place.geometry.location.lat();
        document.getElementById("adress-coordinates-lon").value = place.geometry.location.lng();

        console.log(JSON.stringify(address));
    });
}