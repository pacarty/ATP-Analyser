import axios from 'axios';

const url = 'http://localhost:5000/api/players/';

export const fetchPlayers = async () => {
    const players = await axios.get(url);

    const result = Object.values(players.data);

    return result;
}

export const fetchRankDates = async (id) => {
    const rankDates = await axios.get(url + id);

    const result = Object.values(rankDates.data);

    for (var i = 0; i < result.length; i++) {
        result[i].date = result[i].date.split('T')[0];
    }

    return result;
}

export const fetchDateRange = async (id, sd, ed) => {
    var body = {
        playerId: id, 
        startDate: sd,
        endDate: ed
    };


    const dateRange = await axios.post(url + 'getdaterange', body);

    const result = Object.values(dateRange.data);

    for (var i = 0; i < result.length; i++) {
        result[i].date = result[i].date.split('T')[0];
    }

    return result;
}