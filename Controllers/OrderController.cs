using Microsoft.AspNetCore.Mvc;

class OrderController:Controller
{
    [HttpPost]
    public IActionResult Accept(){
        
        return Content("");
    }

    [HttpPost]
    public IActionResult Cancel(){
        
        return Content("");
    }
}