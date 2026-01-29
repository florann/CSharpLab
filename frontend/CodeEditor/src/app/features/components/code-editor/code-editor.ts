import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import * as monaco from 'monaco-editor';
import { CursorData } from '../../models/cursor-data.interface';

(self as any).MonacoEnvironment = {
  getWorker(_: any, label: string) {
    const baseUrl = window.location.origin;
    
    if (label === 'typescript' || label === 'javascript') {
      return new Worker(`${baseUrl}/assets/monaco-editor/esm/vs/language/typescript/ts.worker.js`, { type: 'module' });
    }
    return new Worker(`${baseUrl}/assets/monaco-editor/esm/vs/editor/editor.worker.js`, { type: 'module' });
  }
};

@Component({
  selector: 'app-code-editor',
  imports: [],
  templateUrl: './code-editor.html',
  styleUrl: './code-editor.css',
})
export class CodeEditor implements AfterViewInit {
  @ViewChild('editContainer') editContainer!: ElementRef;
  private editor?: monaco.editor.IStandaloneCodeEditor;
  private collectionRemoteDecorations: monaco.editor.IEditorDecorationsCollection | undefined = undefined; 
  private collectionCursorDatas: Map<string, [line: number, column: number]> = new Map();

  private connection!: signalR.HubConnection;
  
   ngOnInit() {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5242/cursorhub")
        .build();
      
      // Function to load remote cursors
      this.connection.on('IncomingCursorData', (cursorData: CursorData) => {

        console.log("Incoming cursor data : " , cursorData);

        if(this.collectionCursorDatas.get(cursorData.userName) == undefined) 
        {
          this.collectionCursorDatas.set(cursorData.userName, [cursorData.line, cursorData.column]);
          this.addRemoteCursor();
          return;
        }

        var currentData = this.collectionCursorDatas.get(cursorData.userName);
        if (currentData != undefined && (currentData[0] != cursorData.line || currentData[1] != cursorData.column))
        {
          this.collectionCursorDatas.set(cursorData.userName, [cursorData.line, cursorData.column]);
          this.addRemoteCursor();
          return;
        }
        
        console.log("Nothing to modify");
        console.log("CollectionCursorData : " + this.collectionCursorDatas);
        console.log("cursorData received : " + cursorData);
      });

      this.connection.start()
      .then(() => console.log('Connected!'))
      .catch(err => console.error(err));
    }

  ngAfterViewInit() {
    this.editor = monaco.editor.create(this.editContainer.nativeElement, {
      value: '/* write code here */',
      language: 'javascript',
      theme: 'vs-dark',
      automaticLayout: true
    });

    // Listen to cursor position changes
    this.editor.onDidChangeCursorPosition((e) => {
      console.log('Line:', e.position.lineNumber);
      console.log('Column:', e.position.column);

      this.sendCursorData();
    });

    // Get current position
    const position = this.editor.getPosition();
    console.log(position?.lineNumber, position?.column);

    this.collectionRemoteDecorations  = this.editor?.createDecorationsCollection();
  }

  // Add a decoration for another user's cursor
  addRemoteCursor() {
    if(!this.editor || !this.collectionRemoteDecorations)
      return;

    var collectionRemoteCusorsDecoration: monaco.editor.IModelDeltaDecoration[] = [];

    this.collectionCursorDatas.forEach((cursorData, key) => {
        collectionRemoteCusorsDecoration.push(this.createCursorDecoration(key, cursorData[0], cursorData[1]))
    });

    console.log("Decorations:", collectionRemoteCusorsDecoration);
    this.collectionRemoteDecorations.set(collectionRemoteCusorsDecoration);
  }

  createCursorDecoration(username: string, lineNumber: number, column: number): monaco.editor.IModelDeltaDecoration {
    return {
          range: new monaco.Range(lineNumber, column, lineNumber, column),
          options: {
            className: 'remote-cursor',
            after: {
              content: ` ${username}`,
              inlineClassName: 'remote-cursor-label'
            }
          }
        } as monaco.editor.IModelDeltaDecoration
  }

  async sendCursorData() {
    if(!this.editor)
    {
      console.log("sendCursorData - Editor is undefined");
      return;
    }

    var position = this.editor.getPosition();
    console.log("Send cursor data to hub : " + position);
    console.log("UserId : " +  this.connection.connectionId?.toString());

    await this.connection.invoke('SendCursorData', this.connection.connectionId?.toString(), position?.column, position?.lineNumber);
  }

}
