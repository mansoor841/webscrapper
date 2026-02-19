var PmntTable = document.querySelector(".PmntTable");
var trs = PmntTable.querySelectorAll("tbody tr");
var resultList = [];

for (var i = 0; i < trs.length; i++) {
    var tr = trs[i];
    var tds = tr.querySelectorAll("td");

    if (tds.length > 0) {
        resultList.push({
            BatchType: tds[0].textContent,
            BatchUserID: tds[1].textContent,
            BatchUserName: tds[2].textContent,
            PaymentDate: tds[3].textContent,
            Policy: tds[4].textContent,
            NamedInsured: tds[5].textContent,
            HowPaid: tds[6].textContent,
            Amount: tds[7].textContent,
            BatchNumber: tds[8].textContent,
            AgentID: tds[9].textContent
        });
    }
}

JSON.stringify(resultList);
