import React from 'react';
import { Input, Button } from 'antd';

export default class Register extends React.Component {

    constructor(){
        super();
        this.state = { Name: []};
        this.onAdd = this.onAdd.bind(this);
    }

    onAdd(){
        fetch(`http://localhost:5000/${this.props.controller}/Add?name=${this.state.Name}`,{
            method: "POST",
        }).then( function() {
            this.setState({ Name: ''});
        }.bind(this));
    }

    render(){
        return (
            <div>
                <Input 
                    className="col-md-10" 
                    placeholder="Name" 
                    onChange={ e => this.setState({Name:e.target.value})}
                    value={this.state.Name}/>
                <Button 
                    className="col-md-2" 
                    type="primary" 
                    onClick={this.onAdd}>Add</Button>
            </div>
        );   
    }

}