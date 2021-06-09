function validateSignup()
{
    //validates username
   let name = document.getElementById("u_name")
   if(name.value.length < 1 || name.value.length > 10)
   {
       alert("username must be between 1-10 characters");
       document.getElementById("u_name").focus()
       document.getElementById("u_name").style.borderColor = 'red'
       return false;
   }

   //validates email
   let email = document.getElementById("u_email").value;
   atpos = email.indexOf("@");
   dotpos = email.lastIndexOf(".");
   if (atpos < 1 || ( dotpos - atpos < 2 )) 
   {
      alert("Please enter email ID in valid format");
      document.getElementById("u_email").focus()
      document.getElementById("u_email").style.borderColor = 'red'
      return false;
   }

   //validates password
   let password = document.getElementById("u_pass").value;
   if(password.length < 1 || password.length > 7){
       alert("Password must be between 1-7 characters");
       document.getElementById("u_pass").focus()
       document.getElementById("u_pass").style.borderColor = 'red'
       return false;
   }

   //validates phone number
   let number = document.getElementById("u_number");
   if(isNaN(number.value))
   {
       alert("Enter Numbers Only");
       return false;
   }
   if(number.value.length != 11)
   {
       alert("Contact Number must consist of 11 Numbers");
       document.getElementById("u_number").focus()
       document.getElementById("u_number").style.borderColor = 'red'
       return false;
   }

   //validates ID Card Number
   let id_card_number = document.getElementById("u_nic");
   if(id_card_number.value.length != 15)
   {
       alert("CNIC must contain 15 characters");
       document.getElementById("u_nic").focus()
       document.getElementById("u_nic").style.borderColor = 'red'
       return false;
   }
   if(id_card_number.value[5] != '-' && id_card_number.value[13] != '-')
   {
       alert("Please enter CNIC in valid format");
       document.getElementById("u_nic").focus()
       document.getElementById("u_nic").style.borderColor = 'red'
       return false;
   }

   //validates DOB
   let dob = document.getElementById("u_date")
   if(dob.value.length != 10)
   {
       alert("Please enter correct date");
       document.getElementById("u_date").focus()
       document.getElementById("u_date").style.borderColor = 'red'
       return false;
   }
   if(dob.value[2] != '/' && dob.value[5] != '/')
   {
       alert("Please enter Date in valid format");
       document.getElementById("u_date").focus()
       document.getElementById("u_date").style.borderColor = 'red'
       return false;
   }

   //validates the list for age group
   let x = document.getElementById("age_grp").selectedIndex;
   let y = document.getElementById("age_grp").options;
     if(y[x].index == '0')
     {
       alert("Please Select Your Age Group");
       document.getElementById("age_grp").focus();
       document.getElementById("age_grp").style.borderColor = 'red'
       return false;
     }

     //validates check box for gender
     let gender_a = document.getElementById('gender_1'); 
     let gender_b = document.getElementById('gender_2');  
     if(!(gender_a.checked || gender_b.checked))
     {
       alert("Please specify your gender");
       return false;  
     }

     //validates check box
     let interest_a = document.getElementById('checkbox1'); 
     let interest_b = document.getElementById('checkbox1'); 
     let interest_c = document.getElementById('checkbox3'); 
     if(!(interest_a.checked || interest_b.checked || interest_c.checked))
     {
        alert("Please Speiify atleast 2 interests")
        return false; 
     }   
}