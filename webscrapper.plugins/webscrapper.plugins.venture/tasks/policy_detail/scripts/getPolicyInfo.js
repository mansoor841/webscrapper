const tooltiptext = document.querySelector(".ISiTooltip .tooltiptext");
const elemName = tooltiptext.nextElementSibling;
const lnkDate = document.querySelector("#acct_header_table tr:first-child td:nth-child(3) a")
const lnks = tooltiptext.querySelectorAll("a");
const dates = lnkDate.textContent.split("-");
const obj = {
	Name: elemName.textContent,
	Address: lnks[0].textContent,
	Phone: lnks[1].textContent,
	Email: lnks[3].textContent,
	StartDate: dates[0],
	EndDate: dates[1]
};

JSON.stringify(obj);
