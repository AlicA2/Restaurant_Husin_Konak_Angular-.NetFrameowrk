import { Component, OnInit } from '@angular/core';
import {HubConnection, HubConnectionBuilder, LogLevel} from "@microsoft/signalr";
import {LoginInformacije} from "../../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../../_helpers/autentifikacija-helper";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../moj-config";

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  private hubConnectionBuilder!: HubConnection;
  list: any[] = [];

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  constructor(private httpKlijent:HttpClient) {}
  ngOnInit(): void {
    this.hubConnectionBuilder = new HubConnectionBuilder().withUrl('https://localhost:7273/info').configureLogging(LogLevel.Information).build();
    this.hubConnectionBuilder.start().then(() => console.log('Connection started.......!')).catch(err => console.log('Error while connect with server'));
    this.hubConnectionBuilder.on('SendToAdmin', (result: any) => {
      this.list.push(result);
    });
    if(this.loginInfo().isPremisijaAdmin)
     this.some();

  }

  pogledaj() {
    this.list=[];
    this.httpKlijent.post(MojConfig.adresa_servera + "/api/AdminInfoSignalR", null).subscribe();


  }
  some(){

    var canvas = document.getElementById('neki_canvas') as HTMLCanvasElement;
    let gl = canvas.getContext('experimental-webgl') as WebGL2RenderingContext;

    var vertices = [
      -0.5,0.5,0.0,
      -0.5,-0.5,0.0,
      0.5,-0.5,0.0,
    ];

    let indices = [0,1,2];

    var vertex_buffer = gl.createBuffer();

    gl.bindBuffer(gl.ARRAY_BUFFER, vertex_buffer);

    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);

    gl.bindBuffer(gl.ARRAY_BUFFER, null);

    var Index_Buffer = gl.createBuffer();

    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, Index_Buffer);

    gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(indices), gl.STATIC_DRAW);

    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, null);

    /*================ Shaders ====================*/

    var vertCode =
      'attribute vec3 coordinates;' +

      'void main(void) {' +
      ' gl_Position = vec4(coordinates, 1.0);' +
      '}';

    var vertShader = gl.createShader(gl.VERTEX_SHADER);

    gl.shaderSource(vertShader, vertCode);


    gl.compileShader(vertShader);


    var fragCode =
      'void main(void) {' +
      ' gl_FragColor = vec4(1, 1, 1, 0.1);' +
      '}';


    var fragShader = gl.createShader(gl.FRAGMENT_SHADER);


    gl.shaderSource(fragShader, fragCode);


    gl.compileShader(fragShader);

    var shaderProgram = gl.createProgram();

    gl.attachShader(shaderProgram, vertShader);

    gl.attachShader(shaderProgram, fragShader);

    gl.linkProgram(shaderProgram);

    gl.useProgram(shaderProgram);


    gl.bindBuffer(gl.ARRAY_BUFFER, vertex_buffer);

    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, Index_Buffer);

    var coord = gl.getAttribLocation(shaderProgram, "coordinates");

    gl.vertexAttribPointer(coord, 3, gl.FLOAT, false, 0, 0);

    gl.enableVertexAttribArray(coord);

    gl.clearColor(1, 0, 0, 0.9);

    gl.enable(gl.DEPTH_TEST);

    gl.clear(gl.COLOR_BUFFER_BIT);

    gl.viewport(0,0,canvas.width,canvas.height);

    gl.drawElements(gl.TRIANGLES, indices.length, gl.UNSIGNED_SHORT,0);
  }

}
