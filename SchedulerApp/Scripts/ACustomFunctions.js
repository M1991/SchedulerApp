
/*
var counter = 1;
var limit = 13;
function addInput(divName){
     if (counter == limit)  {
          alert("You have reached the limit of adding " + counter + " inputs");
     }
     else {
          var newdiv = document.createElement('div');
          //newdiv.innerHTML = "Part No " + (counter + 1) + " <br><input type='text' name='soPartNo[]'>" + "Part No " + (counter + 1) + " <br><input type='text' name='soPartNo[]'>";
          //newdiv.innerHTML = "Quantity " + (counter + 1) + " <br><input type='text' name='soPoqty[]'>";
          var newRow = "<tr><td>" + '@Html.TextBoxFor(x => x.Id)' + "</td><td>" + '@Html.TextBoxFor(x => x.Name)' + "</td></tr>";
         // newdiv.innerHTML = '<br><input type="text" name="soPartNo' + counter + '">' + '<input type="text" name="soPoQty' + counter + '">';
          //newdiv.innerHTML = "Quantity " + (counter + 1) + " <br><input type='text' name='myInputs[]'>" + "Part No " + (counter + 1) + " <br>Html.TextBoxFor( Model.SoNoEventds.MultiplePartNo[i])";     
         // newdiv.innerHTML = "Part No " + (counter + 1) + " <br>Html.TextBoxFor( Model.SoNoEventds.MultiplePartNo[i])";

          document.getElementById(divName).appendChild(newRow);
          //document.getElementById(divName).appendChild(newdiv);
          counter++;
     }
} */