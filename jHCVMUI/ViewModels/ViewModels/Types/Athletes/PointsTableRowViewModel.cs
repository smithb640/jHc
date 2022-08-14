﻿namespace jHCVMUI.ViewModels.ViewModels.Types.Athletes
{
    using HandicapModel.AthletesModel;
    using HandicapModel.Interfaces.SeasonModel;
    using System;

    /// <summary>
    /// Class describing a single entry of the points table
    /// </summary>
    public class PointsTableRowViewModel : AthleteBase, IDisposable
    {
        /// <summary>
        /// The athlete details model object for the current season.
        /// </summary>
        private readonly IAthleteSeasonDetails athleteSeasonDetails;

        /// <summary>
        /// Get the athlete points model object for the current season.
        /// </summary>
        private readonly IAthleteSeasonPoints athleteSeasonPoints;

        /// <summary>
        /// Gets the global athlete model object;
        /// </summary>
        private readonly AthleteDetails athleteDetails;

        /// <summary>
        /// Value which indicates whether this object has been disposed.
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Initialises a new instance of the <see cref="PointsTableRowViewModel"/> class.
        /// </summary>
        /// <param name="key">entry key</param>
        /// <param name="name">name of the athlete</param>
        /// <param name="pb">athlete's personal best</param>
        /// <param name="points">total number of points scored</param>
        /// <param name="finishingPoints">number of points scored by finishing an event</param>
        /// <param name="positionPoints">number of points scored through event position</param>
        /// <param name="bestPoints">number of points scored by improving on the season best time</param>
        /// <param name="raceNumber">athlete's number</param>
        /// <param name="numberOfRuns">number of runs started</param>
        /// <param name="averagePoints">average number of points scored per event</param>
        /// <param name="sb">athlete's season best</param>
        public PointsTableRowViewModel(
            IAthleteSeasonDetails athleteSeasonDetails,
            AthleteDetails athleteDetails)
          : base(athleteDetails.Key, athleteDetails.Name)
        {
            this.athleteSeasonDetails = athleteSeasonDetails;
            this.athleteSeasonPoints = athleteSeasonDetails.Points;
            this.athleteDetails = athleteDetails;

            this.PB = this.athleteDetails.PB.ToString();
            this.Points = this.athleteSeasonPoints.TotalPoints;
            this.FinishingPoints = this.athleteSeasonPoints.FinishingPoints;
            this.PositionPoints = this.athleteSeasonPoints.PositionPoints;
            this.BestPoints = this.athleteSeasonPoints.BestPoints;
            this.RaceNumber = this.athleteDetails.PrimaryNumber;
            this.NumberOfRuns = this.athleteSeasonDetails.NumberOfAppearances;
            this.SB = this.athleteSeasonDetails.SB.ToString();

            this.athleteSeasonDetails.ModelUpdateEvent += this.AthleteSeasonDetailsModelUpdateEvent;
            this.athleteSeasonPoints.ModelUpdateEvent += this.AthleteSeasonPointsModelUpdateEvent;
            this.athleteDetails.ModelUpdateEvent += this.AthleteDetailsModelUpdateEvent;
        }

        /// <summary>
        /// Gets or sets the PB.
        /// </summary>
        public string PB { get; private set; }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        public int Points { get; private set; }

        /// <summary>
        /// Gets or sets the finishing points.
        /// </summary>
        public int FinishingPoints { get; private set; }

        /// <summary>
        /// Gets or sets the position points.
        /// </summary>
        public int PositionPoints { get; private set; }

        /// <summary>
        /// Gets or sets the best points.
        /// </summary>
        public int BestPoints { get; private set; }

        /// <summary>
        /// Gets or sets the race number.
        /// </summary>
        public string RaceNumber { get; }

        /// <summary>
        /// Gets or sets the number of runs.
        /// </summary>
        public int NumberOfRuns { get; private set; }

        /// <summary>
        /// Gets or sets the average points.
        /// </summary>
        public string AveragePoints
        {
            get
            {
                double averagePoints = 0;
                if (this.NumberOfRuns > 0)
                {
                    averagePoints = (double)this.Points / this.NumberOfRuns;
                }


                return averagePoints.ToString("0.##");
            }
        }

        /// <summary>
        /// Gets or sets the SB.
        /// </summary>
        public string SB { get; private set; }

        /// <summary>
        /// Dispose this object.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the <see cref="PointsTableRowViewModel"/> class.
        /// </summary>
        /// <param name="disposing">is disposing flag</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.athleteSeasonDetails.ModelUpdateEvent -= this.AthleteSeasonDetailsModelUpdateEvent;
                    this.athleteSeasonPoints.ModelUpdateEvent -= this.AthleteSeasonPointsModelUpdateEvent;
                    this.athleteDetails.ModelUpdateEvent -= this.AthleteDetailsModelUpdateEvent;
                }

                this.disposedValue = true;
            }
        }

        /// <summary>
        /// The <see cref="IAthleteSeasonPoints"/> model has changed. Update the view model with 
        /// the latest information.
        /// </summary>
        /// <param name="sender">the <see cref="IAthleteSeasonPoints"/> object</param>
        /// <param name="e">the evnet arguaments</param>
        private void AthleteSeasonDetailsModelUpdateEvent(object sender, EventArgs e)
        {
            this.NumberOfRuns = this.athleteSeasonDetails.NumberOfAppearances;
            this.SB = this.athleteSeasonDetails.SB.ToString();

            this.RaisePropertyChangedEvent(nameof(this.NumberOfRuns));
            this.RaisePropertyChangedEvent(nameof(this.SB));
            this.RaisePropertyChangedEvent(nameof(this.AveragePoints));
        }

        /// <summary>
        /// The <see cref="IAthleteSeasonPoints"/> model has changed. Update the view model with 
        /// the latest information.
        /// </summary>
        /// <param name="sender">the <see cref="IAthleteSeasonPoints"/> object</param>
        /// <param name="e">the evnet arguaments</param>
        private void AthleteSeasonPointsModelUpdateEvent(object sender, EventArgs e)
        {
            this.Points = this.athleteSeasonPoints.TotalPoints;
            this.FinishingPoints = this.athleteSeasonPoints.FinishingPoints;
            this.PositionPoints = this.athleteSeasonPoints.PositionPoints;
            this.BestPoints = this.athleteSeasonPoints.BestPoints;

            this.RaisePropertyChangedEvent(nameof(this.Points));
            this.RaisePropertyChangedEvent(nameof(this.FinishingPoints));
            this.RaisePropertyChangedEvent(nameof(this.PositionPoints));
            this.RaisePropertyChangedEvent(nameof(this.BestPoints));
            this.RaisePropertyChangedEvent(nameof(this.AveragePoints));
        }

        /// <summary>
        /// The <see cref="AthleteDetails"/> model has changed. Update the view model with 
        /// the latest information.
        /// </summary>
        /// <param name="sender">the <see cref="AthleteDetails"/> object</param>
        /// <param name="e">the evnet arguaments</param>
        private void AthleteDetailsModelUpdateEvent(object sender, EventArgs e)
        {
            this.PB = this.athleteDetails.PB.ToString();

            this.RaisePropertyChangedEvent(nameof(this.PB));
        }
    }
}