



function removetext(myvalue, name1) {
    if (myvalue.classList.contains("Red")) {
          myvalue.classList.remove("Red");
          myvalue.placeholder= name1;
  }
}


$('#assetCreate').click(function (event) {
	if (document.getElementById('worth').value > 0) {
		return true;
	}
	else {
		document.getElementById('worth').value = "";
		
		document.getElementById('worth').placeholder = "Worth cannot be negative";
		event.preventDefault();
		return false;
	}
});


function checkname()
{ 
  
  if(document.getElementById('name').value)
  {
    return true;
  }
  else
  {
   document.getElementById('name').value = "";
    document.getElementById('name').className += " Red";
    document.getElementById('name').placeholder = "Name cannot be null";
    return false; 
  }
}

function checknegative() {

	if (document.getElementById('worth').value > 0) {
		return true;
	}
	else {
		document.getElementById('worth').value = "";
		document.getElementById('worth').className += " Red";
		document.getElementById('worth').placeholder = "Worth cannot be negative";
		return false;
	}
}

function checkid() {
  var inputcard =  document.getElementById('signup-card').value;
  if(inputcard.length == 13 && !isNaN(inputcard))
  {
    return true;
  }
  else
  {
   document.getElementById('signup-card').value = "";
    document.getElementById('signup-card').className += " Red";
    document.getElementById('signup-card').placeholder = "Card must be 13 digits only!";
    return false; 
  }
}

function checkAge() {

    var dob = document.getElementById('signup-age').value;
    if(dob.length == 10) {
    if(dob.charAt(2) == '-' && dob.charAt(5) == '-') {
    var arr1 = dob.split('-');
    var year = arr1[2];
    var age = 2019 - year;

    if(age<18) {
      document.getElementById('signup-age').value = "";
      document.getElementById('signup-age').className += " Red";
      document.getElementById('signup-age').placeholder = "Age should be > 18!";
      return false; 
    }
    else {
      return true;
     }
   }
   else {
    alert("incorrect format of Date!");
    return false;
   }
  }
  else {
    alert("incorrect format of Date!");
    return false;
  }
}

function checkCountry() {
  var inputcountry =  document.getElementById('country').value;
  if(inputcountry == 'Pakistan')
  {
    return true;
  }
  else
  {
   document.getElementById('country').value = "";
    document.getElementById('country').className += " Red";
    document.getElementById('country').placeholder = "Country should be Pakistan!";
    alert("Country should be Pakistan Only!");
    return false; 
  }
}

function checkphone() {
  var inputphone =  document.getElementById('signup-phone').value;
  if(inputphone.length == 11 && !isNaN(inputphone))
  {
    return true;
  }
  else
  {
   document.getElementById('signup-phone').value = "";
    document.getElementById('signup-phone').className += " Red";
    document.getElementById('signup-phone').placeholder = "Number must be 11 digits only!";
    return false; 
  }
}

function checkageGroup() {
	
}
function validateSignup() {
	//validates username
	let name = document.getElementById("signup-username")
	if (name.value.length < 1 || name.value.length > 10) {
		alert("username must be between 1-10 characters");
		document.getElementById("signup-username").focus()
		document.getElementById("signup-username").style.borderColor = 'red'
		return false;
	}

	//validates email
	let email = document.getElementById("signup-email").value;
	atpos = email.indexOf("@");
	dotpos = email.lastIndexOf(".");
	if (atpos < 1 || (dotpos - atpos < 2)) {
		alert("Please enter email ID in valid format");
		document.getElementById("signup-email").focus()
		document.getElementById("signup-email").style.borderColor = 'red'
		return false;
	}

	//validates password
	let password = document.getElementById("u_pass").value;
	if (password.length < 1 || password.length > 7) {
		alert("Password must be between 1-7 characters");
		document.getElementById("u_pass").focus()
		document.getElementById("u_pass").style.borderColor = 'red'
		return false;
	}

	//validates phone number
	let number = document.getElementById("u_number");
	if (isNaN(number.value)) {
		alert("Enter Numbers Only");
		return false;
	}
	if (number.value.length != 11) {
		alert("Contact Number must consist of 11 Numbers");
		document.getElementById("u_number").focus()
		document.getElementById("u_number").style.borderColor = 'red'
		return false;
	}

	//validates ID Card Number
	let id_card_number = document.getElementById("u_nic");
	if (id_card_number.value.length != 15) {
		alert("CNIC must contain 15 characters");
		document.getElementById("u_nic").focus()
		document.getElementById("u_nic").style.borderColor = 'red'
		return false;
	}
	if (id_card_number.value[5] != '-' && id_card_number.value[13] != '-') {
		alert("Please enter CNIC in valid format");
		document.getElementById("u_nic").focus()
		document.getElementById("u_nic").style.borderColor = 'red'
		return false;
	}

	//validates DOB
	let dob = document.getElementById("u_date")
	if (dob.value.length != 10) {
		alert("Please enter correct date");
		document.getElementById("u_date").focus()
		document.getElementById("u_date").style.borderColor = 'red'
		return false;
	}
	if (dob.value[2] != '/' && dob.value[5] != '/') {
		alert("Please enter Date in valid format");
		document.getElementById("u_date").focus()
		document.getElementById("u_date").style.borderColor = 'red'
		return false;
	}

	//validates the list for age group
	let x = document.getElementById("age_grp").selectedIndex;
	let y = document.getElementById("age_grp").options;
	if (y[x].index == '0') {
		alert("Please Select Your Age Group");
		document.getElementById("age_grp").focus();
		document.getElementById("age_grp").style.borderColor = 'red'
		return false;
	}

	//validates check box for gender
	let gender_a = document.getElementById('gender_1');
	let gender_b = document.getElementById('gender_2');
	if (!(gender_a.checked || gender_b.checked)) {
		alert("Please specify your gender");
		return false;
	}

	//validates check box
	let interest_a = document.getElementById('checkbox1');
	let interest_b = document.getElementById('checkbox1');
	let interest_c = document.getElementById('checkbox3');
	if (!(interest_a.checked || interest_b.checked || interest_c.checked)) {
		alert("Please Speiify atleast 2 interests")
		return false;
	}
}

function checkform() {

	//console.log("Hello");
  var uname = document.getElementById('signup-username2').value;
  var email = document.getElementById('signup-email').value;
  var pas = document.getElementById('signup-pass').value;
  var dob1 = document.getElementById('signup-age').value;
  var inputphone1 =  document.getElementById('signup-phone').value;
	


	if (uname && email && pas && dob1 && inputphone1)
	{
    
    var ele = document.getElementsByName('gender'); 
    var flag = 0;        
            for(i = 0; i < ele.length; i++) { 
                if(ele[i].checked) 
                   flag = 1;
            } 
    
		if (flag == 1) {
					   
			alert("Registered Successfully");

			window.open('MainPage', '_self');
			return true;
		}
      else {
		  alert('Select Gender as well!');
		  
      }
      
  }

  else {
    alert("Please Fill the form Completely!");
    return false;
  }



}