import React from "react";
import styled from 'styled-components';
import Bus from './../components/Bus';

const BusWrapper = styled.div`
width: 100%;
min-height: 500px;
height: 100vh;
`;

const BusView = () => {
    return (
        <BusWrapper>
            <Bus />
        </BusWrapper>
    )
}

export default BusView