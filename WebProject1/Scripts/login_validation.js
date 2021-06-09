function validateLoginForm() 
{
	// username is abc
	// password is 123
	
	
    let x = document.forms["my_form"]["username"].value;
    let y = document.forms["my_form"]["password"].value;
    if (x == "") 
    {
      alert("Username field can't be empty. Please fill in username");
      return false;
    }
    if (y == "") 
    {
      alert("Password field can't be empty. Please fill in Password");
      return false;
    }

    //let u_name = document.my_form.username.value;
    //if (u_name != "abc")
    //{
    //  alert("Please enter correct username")
    //  document.my_form.username.focus();
    //  return false;
    //}   

    //var password = document.my_form.password.value;
    //if(password != "123")
    //{
    //  alert("Please enter correct password")
    //  document.my_form.password.focus();
    //  return false;
    //}    
}