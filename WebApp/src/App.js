import React, { Component } from 'react';
import Container from './componnents/Container';
import logo from './logo.svg';
import './App.css';
import { Tabs,Button } from 'antd';

const TabPane = Tabs.TabPane;

class App extends Component {
  render() {
    return (
      <div className="App">
        <Tabs defaultActiveKey="1" >
          <TabPane tab="Developers" key="1">
            <Container controller={"Developers"}/>  
          </TabPane>
          <TabPane tab="Languages" key="2">
            <Container controller={"Languages"}/>  
          </TabPane>
        </Tabs>
      </div>
    );
  }
}

export default App;
