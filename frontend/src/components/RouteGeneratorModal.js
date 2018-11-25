import React from "react";
import {
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Form,
  FormGroup,
  Label,
  Input
} from "reactstrap";
import wretch from "wretch";
import styled from "styled-components";

const NewTransport = styled.div`
  position: absolute;
  top: 15px;
  left: 15px;
  z-index: 1000;
  display: inline-block;
  background-color: #fff;
  font-size: 16px;
  text-transform: uppercase;
  color: #212121;
  margin-left: 15px;
  padding: 15px;
  box-shadow: 0 10px 20px rgba(0,0,0,0.19), 0 6px 6px rgba(0,0,0,0.23);
  font-weight: bolder;
  &:hover {
    text-decoration: none;
    color: #212121;
  }
`

class RouteGeneratorModal extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      modal: false,
      height: "",
      width: "",
      length: "",
      weight: "",
      startPointName: "",
      endPointName: ""
    };
    this.toggle = this.toggle.bind(this);
  }

  toggle() {
    this.setState({
      modal: !this.state.modal
    });
  }

  updateNameField = e => {
    this.setState({
      [e.target.name]: e.target.value
    });
  };

  render() {
    return (
      <div style={{ position: "fixed" }}>
        <NewTransport color="success" onClick={this.toggle}>
          NOWY TRANSPORT
        </NewTransport>
        <Modal isOpen={this.state.modal} toggle={this.toggle}>
          <ModalHeader toggle={this.toggle}>Nowy transport</ModalHeader>
          <Form
            onSubmit={async e => {
              e.preventDefault();
              const res = await wretch(
                "https://orlenapi.azurewebsites.net/Route"
              )
                .post(this.state)
                .json();
              location.href = `http://localhost:3000/map/${res.id}`; //eslint-disable-line
            }}
          >
            <ModalBody>
              <FormGroup>
                <Label for="height">Height</Label>
                <Input
                  type="number"
                  name="height"
                  id="height"
                  placeholder="Height"
                  onChange={this.updateNameField}
                />
              </FormGroup>
              <FormGroup>
                <Label for="width">Width</Label>
                <Input
                  type="number"
                  name="width"
                  id="width"
                  placeholder="Width"
                  onChange={this.updateNameField}
                />
              </FormGroup>
              <FormGroup>
                <Label for="length">Length</Label>
                <Input
                  type="number"
                  name="length"
                  id="length"
                  placeholder="Length"
                  onChange={this.updateNameField}
                />
              </FormGroup>
              <FormGroup>
                <Label for="weight">Weight</Label>
                <Input
                  type="number"
                  name="weight"
                  id="weight"
                  placeholder="Weight"
                  onChange={this.updateNameField}
                />
              </FormGroup>
              <FormGroup>
                <Label for="startPointName">Start</Label>
                <Input
                  type="text"
                  name="startPointName"
                  id="startPointName"
                  placeholder="Start Point Name"
                  onChange={this.updateNameField}
                />
              </FormGroup>
              <FormGroup>
                <Label for="endPointName">End</Label>
                <Input
                  type="text"
                  name="endPointName"
                  id="endPointName"
                  placeholder="End Point Name"
                  onChange={this.updateNameField}
                />
              </FormGroup>
            </ModalBody>
            <ModalFooter>
              <Button color="primary" type="submit">
                Zapisz
              </Button>{" "}
              <Button color="secondary" onClick={this.toggle}>
                Anuluj
              </Button>
            </ModalFooter>
          </Form>
        </Modal>
      </div>
    );
  }
}

export default RouteGeneratorModal;
