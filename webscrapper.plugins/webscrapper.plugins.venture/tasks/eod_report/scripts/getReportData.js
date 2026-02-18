var PmntTable = document.querySelector(".PmntTable");
var trs = PmntTable.querySelectorAll("tbody tr");
var resultList = [];

for (var i = 0; i < trs.length; i++) {
    /*var tr = trs[i];
    var tds = tr.querySelectorAll("td");

    if (tds.length > 0) {
        resultList.push({
            PaymentDate: tds[3].textContent,
            PolicyNo: tds[4].textContent
        });
    }*/
    resultList.push({
            PaymentDate: i+"pd",
            PolicyNo: i+"pn"
        });
}

JSON.stringify(resultList);
