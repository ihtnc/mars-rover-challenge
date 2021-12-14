import { post } from 'core/api';

interface Coordinate {
    x: Number,
    y: Number
}

interface Location {
    position: Coordinate,
    orientation: string
}

interface RoverInput {
    initialLocation: Location,
    commands: Array<string>
}

interface SendRequest {
    landscapeBoundary: Coordinate,
    roverCommands: Array<RoverInput>
}

interface RoverOutput {
    id: string,
    location: Location
}

interface SendResponse {
    roverOutputs: Array<RoverOutput>
}

export const postInput = async (payload: string): Promise<string> => {
    const request = parseInput(payload);
    const response = await post<SendResponse>('/api/send', request);
    const output = parseOutput(response);
    return output;
}

function parseInput(value: string): SendRequest {
    const lines = value.split(new RegExp("[\\r|\\n]+"));
    const boundary = lines[0];

    const commands: Array<RoverInput> = [];
    for(let i = 1; i < lines.length; i+=2) {
        const location = lines[i];
        commands.push({
            initialLocation: parseLocation(location),
            commands: lines[i+1].split('')
        })
    }

    const request: SendRequest = {
        landscapeBoundary: parseCoordinate(boundary),
        roverCommands: commands
    };

    return request;
}

function parseCoordinate(value: string): Coordinate {
    const coordinates = value.split(new RegExp("[\\s]+"));
    if (coordinates.length !== 2) { throw new Error("Invalid coordinate format."); }

    return {
        x: Number(coordinates[0]),
        y: Number(coordinates[1])
    }
}

function parseLocation(value: string): Location {
    const location = value.split(new RegExp("[\\s]+"));
    if (location.length !== 3) { throw new Error("Invalid location format."); }

    return {
        position: parseCoordinate(`${location[0]} ${location[1]}`),
        orientation: location[2]
    }
}

function parseOutput(value: SendResponse): string {
    let output = '';
    value.roverOutputs.forEach(rover => {
        output += `${rover.location.position.x} ${rover.location.position.y} ${rover.location.orientation}\n`;
    });
    return output;
}