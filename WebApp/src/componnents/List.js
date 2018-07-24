import React from 'react';
import { Button,Table  } from 'antd';

export default class List extends React.Component {
    
    constructor(props) {
        super(props);
        this.state = { data: []};
        this.onDelete = this.onDelete.bind(this);
    }

    componentDidMount(){
        this.socket = new WebSocket(`ws://localhost:5000/${this.props.controller}/Subscribe`);  
        this.socket.onopen = function (event) {  
            console.log('open',event);
        };  

        this.socket.onclose = function (event) {  
            console.log('close',event);
        };  

        this.socket.onerror = function (event) {  
            console.log('error',event);
        };  
        this.socket.onmessage = function (event) {  
            console.log('message',event);
            this.setState( { data: JSON.parse(event.data) });
        }.bind(this);  
    }

    onDelete(id){
        fetch(`http://localhost:5000/${this.props.controller}/Delete?id=${id}`,{
            method: "DELETE",
        })
    }

    render() {

        const columns = [{
            title: 'Id',
            dataIndex: 'Id',
            key: 'Id',
          }, {
            title: 'Name',
            dataIndex: 'Name',
            key: 'Name',
          }, {
            title: 'Action',
            key: 'action',
            render: (text, record) => (
                <Button type="danger" onClick={ () => this.onDelete(record.Id) }>Delete</Button>
            ),
          }];

        return (
            <div className="row">
               <Table className="col-md-12" pagination={{ pageSize: 5 }} columns={columns} dataSource={this.state.data} />
            </div>
        );
    }
}
