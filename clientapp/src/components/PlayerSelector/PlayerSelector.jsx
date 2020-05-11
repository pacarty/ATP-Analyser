import React from 'react';
import TextField from '@material-ui/core/TextField';
import Autocomplete from '@material-ui/lab/Autocomplete';

import { Displayer } from '../../components';
import { fetchPlayers, fetchRankDates, fetchDateRange } from '../../api';

class PlayerSelector extends React.Component {
    state = {
        playerData: [],
        currentPlayerId: 'exampleId',
        startDate: "1970-01-01",
        endDate: "2070-12-31",
        rangedData: []
    }

    async componentDidMount() {
        const players = await fetchPlayers();

        this.setState({
            playerData: players,
            currentPlayerId: 'exampleId',
        });

        this.resetDates();
    }

    handlePlayerChange = (event, values) => {
        if (values == null) {
            this.setState({currentPlayerId: 'exampleId'});
        } else {
            const newPlayer = values.playerId;
        
        this.setState({currentPlayerId: newPlayer});
        }

        this.resetDates();

    }

    handleStartChange = (event) => {
        this.setState({startDate: event.target.value});
        this.setRange();
    }

    handleEndChange = (event) => {
        this.setState({endDate: event.target.value});
        this.setRange();
    }

    resetDates = async () => {
        const playerRankDates = await fetchRankDates(this.state.currentPlayerId);

        this.setState({
            startDate: playerRankDates[0].date,
            endDate: playerRankDates[playerRankDates.length - 1].date
        });

        this.setRange();
    }

    setRange = async () => {
        const theRange = await fetchDateRange(
            this.state.currentPlayerId,
            this.state.startDate,
            this.state.endDate
        );

        this.setState({rangedData: theRange});
    }
    
    render() {
        const playerList = this.state.playerData;

        if (playerList.length < 1)
        {
            return (
                <div>
                    <p>Loading Players</p>
                </div>
            );
        }

        return (
            <div>
            <div>
                <Autocomplete
                id="combo-box-demo"
                options={playerList}
                getOptionLabel={(option) => option.name}
                style={{ width: 300 }}
                onChange={this.handlePlayerChange}
                renderInput={(params) => <TextField {...params} label="Select Player" variant="outlined" />}
                />

                <br/><br/>
                Between dates:

                <input type="date"
                value={this.state.startDate}
                onChange={this.handleStartChange}
                />
                to
                <input type="date"
                value={this.state.endDate}
                onChange={this.handleEndChange}
                />

                <br/><br/>

                <button onClick={this.resetDates}>Reset Dates</button>

                </div>
                <div>
                <Displayer x = {this.state.rangedData} />
                </div>
                </div>
        );
    }
}

export default PlayerSelector;