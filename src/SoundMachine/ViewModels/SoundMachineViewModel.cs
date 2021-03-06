﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Linq;
using System.Reactive;
using JaybirdLabs.Chirp;
using ReactiveUI;

namespace SoundMachine.ViewModels
{
    public class SoundMachineViewModel : ViewModelBase
    {
        public const string WaveFileGroup = nameof(WaveFileGroup);
        public const string FlacFileGroup = nameof(FlacFileGroup);
        public const string GenWaveGroup = nameof(GenWaveGroup);

        private readonly List<SoundPlayerViewModelBase> _allSoundVms = new List<SoundPlayerViewModelBase>();

        private int _selectedSegment;

        private ObservableCollection<SoundPlayerViewModelBase> _soundPlayers;

        public SoundMachineViewModel()
        {
            _allSoundVms.Add(
                new FilePlayerViewModel("SoundMachine.Audio.Cannon.wav", "Cannon Wave File", WaveFileGroup));
            _allSoundVms.Add(new FilePlayerViewModel("SoundMachine.Audio.TriangleWave.wav", "Triangle Wave File",
                WaveFileGroup));
            _allSoundVms.Add(new FilePlayerViewModel("SoundMachine.Audio.SawWave.wav", "Saw Wave File", WaveFileGroup));
            _allSoundVms.Add(new FilePlayerViewModel("SoundMachine.Audio.PositiveShortDigital.flac", "Positive Sound",
                FlacFileGroup));
            _allSoundVms.Add(new FilePlayerViewModel("SoundMachine.Audio.NegativeShortDigital.flac", "Negative Sound",
                FlacFileGroup));
            _allSoundVms.Add(new FilePlayerViewModel("SoundMachine.Audio.BeachBird.flac", "Beach Birds",
                FlacFileGroup));
            _allSoundVms.Add(new FilePlayerViewModel("SoundMachine.Audio.UrbanBird.flac", "Urban Birds",
                FlacFileGroup));
            _allSoundVms.Add(new WaveFormPlayerViewModel("Sine Wave", GenWaveGroup, 300, SignalGeneratorType.Sin, 5));
            _allSoundVms.Add(new WaveFormPlayerViewModel("Triangle  Wave", GenWaveGroup, 300,
                SignalGeneratorType.Triangle, 5));
            _allSoundVms.Add(
                new WaveFormPlayerViewModel("Saw Wave", GenWaveGroup, 300, SignalGeneratorType.SawTooth, 5));
            _allSoundVms.Add(new WaveFormPlayerViewModel("Square Wave", GenWaveGroup, 300, SignalGeneratorType.Square,
                5));
            _allSoundVms.Add(
                new WaveFormPlayerViewModel("White Noise", GenWaveGroup, 300, SignalGeneratorType.White, 5));

            this.WhenAnyValue(x => x.SelectedSegment)
                .Select(IndexToGroup)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(groupName =>
                {
                    SoundPlayers =
                        new ObservableCollection<SoundPlayerViewModelBase>(
                            _allSoundVms.Where(vm => vm.GroupName == groupName));
                })
                .DisposeWith(Disposables);
        }

        public int SelectedSegment
        {
            get => _selectedSegment;
            set => this.RaiseAndSetIfChanged(ref _selectedSegment, value);
        }

        public ReactiveCommand<Unit, Unit> SegmentSeletedCommand { get; }

        public ObservableCollection<SoundPlayerViewModelBase> SoundPlayers
        {
            get => _soundPlayers;
            set => this.RaiseAndSetIfChanged(ref _soundPlayers, value);
        }

        private static string IndexToGroup(int index)
        {
            var group = "";
            switch (index)
            {
                case 0:
                    group = GenWaveGroup;
                    break;
                case 1:
                    group = WaveFileGroup;
                    break;
                case 2:
                    group = FlacFileGroup;
                    break;
            }

            return group;
        }
    }
}