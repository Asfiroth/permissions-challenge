import { Grid, Icon } from "@mui/material";

const ErrorMessage = ({message} : {message: string}) => {
    return (
        <>
            <Grid container spacing={2}>
                <Icon color="error">error</Icon>
                <Grid item xs={12}>
                    {message}
                </Grid>
            </Grid>
        </>
    )
};

export default ErrorMessage;