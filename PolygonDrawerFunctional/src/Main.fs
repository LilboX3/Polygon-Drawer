module App

open Feliz
open Elmish
open Elmish.React
open Elmish.HMR
 
   
let init,update,render = 
    PolygonDrawing.init, PolygonDrawing.update, PolygonDrawing.render
 
Program.mkProgram init update render // Elmish to manage states and rendering (no manual redraw etc)
|> Program.withReactSynchronous "feliz-app"
|> Program.run