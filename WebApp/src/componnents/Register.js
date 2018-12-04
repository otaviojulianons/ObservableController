import React from 'react';
import { Input, Button } from 'antd';

const URL_API = process.env.REACT_APP_API_URL;
export default class Register extends React.Component {

    constructor(){
        super();
        this.state = { Name: []};
        this.onAdd = this.onAdd.bind(this);
    }

    onAdd(){
        fetch(`http://${URL_API}/${this.props.controller}/Add?name=${this.state.Name}`,{
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