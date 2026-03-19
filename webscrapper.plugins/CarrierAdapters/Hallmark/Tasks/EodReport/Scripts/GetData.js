const pmntTable = document.querySelector(".PmntTable");
const trs = pmntTable.querySelectorAll("tbody tr");
const resultList = [];

for (var i = 0; i < trs.length; i++) {
    const tds = trs[i].querySelectorAll("td");

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
