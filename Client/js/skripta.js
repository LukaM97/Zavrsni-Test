var host = "https://localhost:";
var port = "44304/";
var agencijeEndpoint = "api/Agencije/";
var oglasiEndpoint = "api/Oglasi/";
var loginEndpoint = "api/authentication/login";
var registerEndpoint = "api/authentication/register";
var pretragaOglasaEndpoint = "api/pretraga";
var formAction = "Create";
var editingId;
var jwt_token;

function showLogin(){
    document.getElementById("loginForm").style.display = "block";
    document.getElementById("registerForm").style.display = "none";
    document.getElementById("index").style.display = "none";
}

function showRegistration(){
    document.getElementById("loginForm").style.display = "none";
    document.getElementById("registerForm").style.display = "block";
    document.getElementById("index").style.display = "none";

}

function quitForm(){
    document.getElementById("loginForm").style.display = "none";
    document.getElementById("registerForm").style.display = "none";
    document.getElementById("index").style.display = "block";

}

function loadOglasi(){

    var requestUrl = host + port + oglasiEndpoint;
    var headers = { 'Content-Type': 'application/json' };
	console.log("URL zahteva: " + requestUrl);
	fetch(requestUrl,{headers:headers})
		.then((response) => {
			if (response.status === 200) {
				response.json().then(setOglasi);
			} else {
				console.log("Error occured with code " + response.status);
				//showError();
			}
		})
		.catch(error => console.log(error));
}

function loadAgencije() {
	var requestUrl = host + port + agencijeEndpoint;
	console.log("URL zahteva: " + requestUrl);
	fetch(requestUrl)
		.then((response) => {
			if (response.status === 200) {
				response.json().then(setAgencije);
			} else {
				console.log("Error occured with code " + response.status);
			}
		})
		.catch(error => console.log(error));
};

function setOglasi(data){
    var container = document.getElementById("data");

    console.log(data);

    var h1 = document.createElement("h1");
    container.appendChild(h1);
	h1.innerHTML = "Oglasi za nekretnine";
    container.appendChild(document.createElement("br"));
    var table = document.createElement("table");
	table.className = "table table-hover";
    table.style = "width: 60%";
    var tableBody = document.createElement("tbody");
    var tableHeader = document.createElement("thead");
	tableHeader.className = "prviHeader";
    
	var row = document.createElement("tr");
    var th1 = document.createElement("th") //.appendChild(document.createTextNode("ID"));
	var th1Text = document.createTextNode("Naslov");
	th1.appendChild(th1Text);
    var th2 = document.createElement("th") //.appendChild(document.createTextNode("Ime"));
	var th2Text = document.createTextNode("Cena");
	th2.appendChild(th2Text);
    var th3 = document.createElement("th") //.appendChild(document.createTextNode("Prezime"));
	var th3Text = document.createTextNode("Tip nekretnine");
	th3.appendChild(th3Text);
    var th4 = document.createElement("th") //.appendChild(document.createTextNode("Prezime"));
	var th4Text = document.createTextNode("Agencija");
	th4.appendChild(th4Text);

    row.appendChild(th1);
    row.appendChild(th2);
    row.appendChild(th3);
    row.appendChild(th4);

    if(jwt_token){
            var th6 = document.createElement("th") 
	        var th6Text = document.createTextNode("Godina izgradnje");
	        th6.appendChild(th6Text);
            row.appendChild(th6);
            var th7 = document.createElement("th") 
	        var th7Text = document.createTextNode("Akcija");
	        th7.appendChild(th7Text);
            row.appendChild(th7);
    }

	tableHeader.appendChild(row);

    for(var i=0; i<data.length; i++){
        var row1 = document.createElement("tr");

        var td1 = document.createElement("td") //.appendChild(document.createTextNode(data[i].id));
		var td1Text = document.createTextNode(data[i].naslov);
		td1.appendChild(td1Text);
        var td2 = document.createElement("td") //.appendChild(document.createTextNode(data[i].ime));
		var td2Text = document.createTextNode(data[i].cenaNekretnine);
		td2.appendChild(td2Text);
        var td3 = document.createElement("td") //.appendChild(document.createTextNode(data[i].prezime));
		var td3Text = document.createTextNode(data[i].tip);
		td3.appendChild(td3Text);
        var td4 = document.createElement("td") //.appendChild(document.createTextNode(data[i].prezime));
		var td4Text = document.createTextNode(data[i].agencija.naziv);
		td4.appendChild(td4Text);

        row1.appendChild(td1);
        row1.appendChild(td2);
        row1.appendChild(td3);
        row1.appendChild(td4);

        if (jwt_token) {
            var td4 = document.createElement("td") //.appendChild(document.createTextNode(data[i].prezime));
		    var td4Text = document.createTextNode(data[i].agencija.godinaOsnivanja);
		    td4.appendChild(td4Text);
            row1.appendChild(td4);

            var stringId = data[i].id.toString();

            var buttonDelete = document.createElement("button");
            buttonDelete.name = stringId;
            buttonDelete.addEventListener("click", deleteOglas);
            var buttonDeleteText = document.createTextNode("obrisi");
            buttonDelete.appendChild(buttonDeleteText);
            var buttonDeleteCell = document.createElement("td");
            buttonDeleteCell.appendChild(buttonDelete);
            buttonDelete.style.backgroundColor = "red";
            row1.appendChild(buttonDeleteCell);	

        }

        tableBody.appendChild(row1);
    }
    
    table.appendChild(tableHeader);
    table.appendChild(tableBody);

    
    container.appendChild(table);
}

function setAgencije(data) {
	var dropdown = document.getElementById("oglasAgencija");
	dropdown.innerHTML = "";
	for (var i = 0; i < data.length; i++) {
		var option = document.createElement("option");
		option.value = data[i].id;
		var text = document.createTextNode(data[i].naziv);
		option.appendChild(text);
		dropdown.appendChild(option);
	}
}

function validateRegisterForm(username,email,password,confirmPassword){
    var usernameError = document.getElementById("usernameRegError");
    usernameError.innerHTML = "";
    var emailError = document.getElementById("emailRegError");
    emailError.innerHTML = "";
    var passwordError = document.getElementById("passwordRegError");
    passwordError.innerHTML = "";
    var confirmPassError = document.getElementById("confirmPassRegError");
    confirmPassError.innerHTML = "";

    var isValid = true;
    if(!username){
        usernameError.innerHTML = "This field can not be empty!";
        isValid = false;
    }
    if(!email){
        emailError.innerHTML = "This field can not be empty!";
        isValid = false;
    }
    if(!password){
        passwordError.innerHTML = "This field can not be empty!";
        isValid = false;
    }
    if(!confirmPassword){
        confirmPassError.innerHTML = "This field can not be empty!";
        isValid = false;
    }
    else if(confirmPassword != password){
        confirmPassError.innerHTML = "The input value does not match the value of password!";
        isValid = false;
    }

    return isValid;
}

function registerUser() {
	var username = document.getElementById("usernameRegister").value;
	var email = document.getElementById("emailRegister").value;
	var password = document.getElementById("passwordRegister").value;
	var confirmPassword = document.getElementById("confirmPasswordRegister").value;

	if (validateRegisterForm(username, email, password, confirmPassword)) {
		var url = host + port + registerEndpoint;
		var sendData = { "Username": username, "Email": email, "Password": password };
		fetch(url, { method: "POST", headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200) {
					document.getElementById("registerForm");
					console.log("Successful registration");
					showLogin();
				} else {
					console.log("Error occured with code " + response.status);
					console.log(response);
					alert("Greska prilikom registracije!");
					response.text().then(text => { console.log(text); })
				}
			})
			.catch(error => console.log(error));
	}
	return false;
}

function validateLoginForm(username,password){
    var usernameError = document.getElementById("usernameLoginError");
    usernameError.innerHTML = "";
    var passwordError = document.getElementById("passwordLoginError");
    passwordError.innerHTML = "";

    var isValid = true;
    if(!username){
        usernameError.innerHTML = "This field can not be empty!";
        isValid = false;
    }
    if(!password){
        passwordError.innerHTML = "This field can not be empty!";
        isValid = false;
    }

    return isValid;
}

function loginUser(){
    var username = document.getElementById("usernameLogin").value;
    var password = document.getElementById("passwordLogin").value;

    if(validateLoginForm(username,password)){
        var url = host + port + loginEndpoint;
		var sendData = { "Username": username, "Password": password };
		fetch(url, { method: "POST", headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200) {
					console.log("Successful login");
					response.json().then(function (data) {
						console.log(data);
						document.getElementById("info").innerHTML = "Prijavljeni korisnik: <b>" + data.username + "<b/>.";
                        document.getElementById("infoForm").style.display = "block";
                        document.getElementById("loginForm").style.display = "none";
						jwt_token = data.token;
                        document.getElementById("data").innerHTML = "";
						loadOglasi();
                        loadAgencije();
                        document.getElementById("pretragaForm").style.display = "block";
                        document.getElementById("kreirajForm").style.display = "block";
                        //document.getElementById("line1").style.display = "block";
					});
				} else {
					console.log("Error occured with code " + response.status);
					console.log(response);
					alert("Greska prilikom prijave!");
				}
            })
			.catch(error => console.log(error));
    }
    return false;
}

function validateOglas(naslov,tip,godina,cena){
    var naslovError = document.getElementById("naslovError");
    naslovError.innerHTML = "";
    var tipError = document.getElementById("tipError");
    tipError.innerHTML = "";
	var godinaError = document.getElementById("godinaError");
    godinaError.innerHTML = "";
	var cenaError = document.getElementById("cenaError");
    cenaError.innerHTML = "";

    var isValid = true;
    if(!naslov){
        naslovError.innerHTML = "This field can not be empty!";
        isValid = false;
    }
	else if(naslov.length < 2 || naslov.length > 100){
		naslovError.innerHTML = "Naslov moze imati najmanje 2 a najvise 100 karaktera!";
		isValid = false;
	}
    if(!tip){
        tipError.innerHTML = "This field can not be empty!";
        isValid = false;
    }
	else if(tip.length < 2 || tip.length > 20){
		tipError.innerHTML = "Tip moze imati najmanje 2 a najvise 20 karaktera!";
        isValid = false;
	}
	if(!godina){
        godinaError.innerHTML = "This field can not be empty!";
        isValid = false;
    }
	else if(godina < 1910 || godina > 2022){
		godinaError.innerHTML = "Godina mora biti izmedju 1910 i 2022!";
        isValid = false;
	}
	if(!cena){
        cenaError.innerHTML = "This field can not be empty!";
        isValid = false;
    }
	else if(cena < 10000 || cena > 300000){
		cenaError.innerHTML = "Cena mora biti izmedju 10 000 i 300 000!";
        isValid = false;
	}

    return isValid;
}

function createOglas(){
    var oglasNaslov = document.getElementById("oglasNaslov").value;
	var oglasTip = document.getElementById("oglasTip").value;
	var oglasGodina = document.getElementById("oglasGodina").value;
    var oglasCena = document.getElementById("oglasCena").value;
    var oglasAgencija = document.getElementById("oglasAgencija").value;
	var httpAction;
	var sendData;
	var url;

	if(validateOglas(oglasNaslov,oglasTip,oglasGodina,oglasCena)){
		var headers = { 'Content-Type': 'application/json' };
		if (jwt_token) {
			headers.Authorization = 'Bearer ' + jwt_token;		
		}

		if (formAction === "Create") {
			httpAction = "POST";
			url = host + port + oglasiEndpoint;
			sendData = {
				"Naslov": oglasNaslov,
				"Tip": oglasTip,
				"GodinaIzgradnje": oglasGodina,
				"CenaNekretnine": oglasCena,
				"AgencijaId": oglasAgencija
			};
		}

		console.log("Objekat za slanje");
		console.log(sendData);

		fetch(url, { method: httpAction, headers: headers, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200 || response.status === 201) {
					console.log("Successful action");
					formAction = "Create";
					document.getElementById("data").innerHTML = "";
					loadOglasi();
					document.getElementById("oglasNaslov").value = "";
					document.getElementById("oglasTip").value = "";
					document.getElementById("oglasGodina").value = "";
					document.getElementById("oglasCena").value = "";
				} else {
					console.log("Error occured with code " + response.status);
					alert("Error occured!");
				}
			})
			.catch(error => console.log(error));
	}
	return false;
}

function deleteOglas(){
    var deleteID = this.name;
	var url = host + port + oglasiEndpoint + deleteID.toString();
	var headers = { 'Content-Type': 'application/json' };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;		
	}
	fetch(url, { method: "DELETE", headers: headers})
		.then((response) => {
			if (response.status === 204) {
				console.log("Successful action");
                document.getElementById("data").innerHTML = "";
				loadOglasi();
			} else {
				console.log("Error occured with code " + response.status);
				alert("Desila se greska!");
			}
		})
		.catch(error => console.log(error));
}

function pretragaOglasa(){
    var najmanje = document.getElementById("pretragaNajmanje").value;
    var najvise = document.getElementById("pretragaNajvise").value;
    var sendData = {
        "Najmanje":najmanje,
        "Najvise":najvise,
    };

    var url = host + port + pretragaOglasaEndpoint;
    var headers = { 'Content-Type': 'application/json' };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;		
	}

    console.log("Objekat za slanje");
	console.log(sendData);

    if(!najmanje && !najvise){
        document.getElementById("data").innerHTML = "";
        loadOglasi();
        return false;
    }
	fetch(url, { method: "POST", headers: headers, body: JSON.stringify(sendData) })
		.then((response) => {
			if (response.status === 200) {
                document.getElementById("data").innerHTML = "";
				response.json().then(setOglasi);
			} else {
				console.log("Error occured with code " + response.status);
				alert("Desila se greska prilikom pretrage!");
			}
		})
		.catch(error => console.log(error));
	return false;
}

function clearForm(){
    document.getElementById("oglasNaslov").value = "";
	document.getElementById("naslovError").innerHTML = "";
    document.getElementById("oglasTip").value = "";
	document.getElementById("tipError").innerHTML = "";
    document.getElementById("oglasGodina").value = "";
	document.getElementById("godinaError").innerHTML = "";
    document.getElementById("oglasCena").value = "";
	document.getElementById("cenaError").innerHTML = "";

    return false;
}