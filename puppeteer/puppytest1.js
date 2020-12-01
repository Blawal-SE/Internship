const puppeteer = require("puppeteer");
(async () => {
  const browser = await puppeteer.launch({
    headless: false,
    slowMo:80
  });
  try {
  const page = await browser.newPage();
  await page.setViewport({width: 1366, height: 768});

  await page.goto("file:///D:/Internship/jquery/Login.html");
  await page.evaluate( () => document.getElementById("userName").value = "");
  await  page.type('#userName','b761');
  await page.evaluate( () => document.getElementById("password").value = "");
  await page.type('#password', '761');

  await  page.click("#login");
 
      
  await page.click('.addNewStudent');
  await page.type('#Name', 'puppyteertest');
  await page.type('#FName', 'puppysfather');
  await page.type('#email', 'puppy@gmail.com');
  await page.type('#DOB', '11041995');
  await page.select("#courseslct", 'OOP', 'C#', 'WEB');
  await page.type('#phone', '03458879817');
  await page.type('#Password', '123456789');
  await page.type('#ConfrimPassword', '123456789');
  let image='https://p.bigstockphoto.com/GeFvQkBbSLaMdpKXF1Zv_bigstock-Aerial-View-Of-Blue-Lakes-And--227291596.jpg';
  await page.click('#saveBtn');

  await page.goto("file:///D:/Internship/jquery/studentList.html");
  await page.click('.editNewWindowBtn');

  await page.type('#Name', 'puppyteertestEditing');
  await page.type('#FName', 'puppysfatherEditing');
  await page.type('#email', 'Editingpuppy@gmail.com');
  await page.type('#DOB', '11042000');
  await page.select("#courseslct", 'OOP', 'C#', 'WEB');
  await page.type('#phone', '0000000000');
  await page.type('#Password', '123456789');
  await page.type('#ConfrimPassword', '123456789');
  
  await page.goto("file:///D:/Internship/jquery/studentList.html");
  await page.click('.deleteBtn');
  
  } catch (error) {
      console.log(error);
  }
  
  await page.screenshot({ path: "loginsuccess.png" });
  await browser.close();

})();
