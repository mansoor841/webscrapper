const pmntTable1 = document.querySelector(".PmntTable1");
const trs = pmntTable1.querySelectorAll("tr");
const resultList = [];
var j = 0;

for (var i = 0; i < trs.length; i++) {
    const tds = trs[i].querySelectorAll("td");

    if (tds.length > 0) {
        const divId  = "div#SCREENUWVehicleInfo" + j;
        const vehicleInfoDiv = document.querySelector(divId);
        const viTrs = vehicleInfoDiv.querySelectorAll("tr");
        const viTds1 = viTrs[1].querySelectorAll("td");
        const viTds2 = viTrs[2].querySelectorAll("td");

        resultList.push({
            Year: tds[0].textContent,
            Make: tds[1].textContent,
            Model: tds[2].textContent,
            VIN: tds[3].querySelector("a").textContent,
            VehicleColor: viTds1[0].textContent,
            TagNumber: viTds2[0].textContent,
            BodyStyle: viTds2[1].textContent
        });

        j++;
    }
}

JSON.stringify(resultList);