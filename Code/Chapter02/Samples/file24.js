var countryList = "AF,Afghanistan,AL,Albania,DZ,Algeria,US,United States";
function DisplayCountries(selected) {
  var countries = countryList.split(",");
  var count = countries.length;
  var i = 0;
  document.write('<select>');
  while(i < count) {
    document.write('<option value="');
    document.write(countries[i]);
    document.write('"');
    document.write(countries[i] == selected ? ' selected' : "");
    document.write(">");
    document.write(countries[i+1]);
    document.write('</option>');
    i=i+2;
  }
  document.write('</select>');
}

