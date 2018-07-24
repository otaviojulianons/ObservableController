import React from 'react';
import List from  './List';
import Register from  './Register';
import { Card  } from 'antd';

export default class Container extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    render() {
        return (
            <div className="container">
                <div className="col-md-12">
                    <Card title={this.props.controller}>
                        <Register controller={this.props.controller}/>  
                        <List controller={this.props.controller}/>
                    </Card>
                </div>  
            </div>                
        );
    }
}